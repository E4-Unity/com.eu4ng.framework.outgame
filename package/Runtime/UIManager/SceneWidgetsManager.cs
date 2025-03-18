using System.Collections.Generic;
using Eu4ng.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Eu4ng.Framework.OutGame
{
    /// <summary>
    /// UI Manager 클래스의 컴포넌트 클래스입니다.
    /// 씬 마다 설정된 StartupWidgets 관리를 담당합니다.
    /// </summary>
    public class SceneWidgetsManager : MonoBehaviour
    {
        [Header("State")]
        [SerializeField, ReadOnly] List<RectTransform> m_GlobalStartupWidgets = new List<RectTransform>();
        [SerializeField, ReadOnly] List<RectTransform> m_SceneStartupWidgets = new List<RectTransform>();

        OutGameFrameworkSettings Settings => OutGameFrameworkSettings.Instance;
        IUIManager UIManagerInterface => UIManager.Instance;

        protected virtual void Awake()
        {
            CreateGlobalWidgets();

            SceneManager.activeSceneChanged += OnActiveSceneChanged;
        }

        protected virtual void CreateGlobalWidgets()
        {
            if (m_GlobalStartupWidgets.Count > 0) return;

            foreach (var globalStartupWidgetPrefab in Settings.GlobalStartupWidgetPrefabs)
            {
                UIManagerInterface.ShowWidget(globalStartupWidgetPrefab);
                var globalStartupWidget = UIManagerInterface.GetWidget(globalStartupWidgetPrefab);
                m_GlobalStartupWidgets.Add(globalStartupWidget);

                // 위젯 프리팹 설정과 관계없이 위젯 인스턴스의 IsGlobalWidget를 true로 설정
                var userWidgetInterface = globalStartupWidget.GetComponent<IUserWidget>();
                if (!userWidgetInterface.IsGlobalWidget)
                {
                    userWidgetInterface.IsGlobalWidget = true;

                    LogOutGameFramework.LogWarning(globalStartupWidgetPrefab.name + " is not set as global widget. Force set widgetInstance as global widget.");
                }
            }
        }

        protected virtual void OnActiveSceneChanged(Scene currentScene, Scene nextScene)
        {
            DestroySceneWidgets();

            CreateSceneWidgets(nextScene.buildIndex);
        }

        protected virtual void DestroySceneWidgets()
        {
            // DynamicWidgetManager에서 자동으로 제거되므로 m_SceneStartupWidgets 목록만 초기화
            m_SceneStartupWidgets.Clear();
        }

        protected virtual void CreateSceneWidgets(int buildIndex)
        {
            var sceneStartupWidgetPrefabs = Settings.GetSceneStartupWidgetPrefabs(buildIndex);
            foreach (var sceneStartupWidgetPrefab in sceneStartupWidgetPrefabs)
            {
                UIManagerInterface.ShowWidget(sceneStartupWidgetPrefab);
                var sceneStartupWidget = UIManagerInterface.GetWidget(sceneStartupWidgetPrefab);
                m_SceneStartupWidgets.Add(sceneStartupWidget);

                // 위젯 프리팹 설정과 관계없이 위젯 인스턴스의 IsGlobalWidget를 false로 설정
                var userWidgetInterface = sceneStartupWidget.GetComponent<IUserWidget>();
                if (userWidgetInterface.IsGlobalWidget)
                {
                    userWidgetInterface.IsGlobalWidget = false;

                    LogOutGameFramework.LogWarning(sceneStartupWidget.name + " is set as global widget. Force set widgetInstance as scene widget.");
                }
            }
        }
    }
}
