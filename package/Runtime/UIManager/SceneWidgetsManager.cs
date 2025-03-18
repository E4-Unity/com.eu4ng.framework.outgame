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
                var globalStartupWidget = UIManagerInterface.GetWidgetInstance(globalStartupWidgetPrefab);
                m_GlobalStartupWidgets.Add(globalStartupWidget);
            }
        }

        protected virtual void OnActiveSceneChanged(Scene currentScene, Scene nextScene)
        {
            DestroySceneWidgets();

            CreateSceneWidgets(nextScene.buildIndex);
        }

        protected virtual void DestroySceneWidgets()
        {
            foreach (var sceneWidget in m_SceneStartupWidgets)
            {
                IUserWidget userWidgetInterface = sceneWidget.GetComponent<IUserWidget>();
                userWidgetInterface?.Remove();
            }

            m_SceneStartupWidgets.Clear();
        }

        protected virtual void CreateSceneWidgets(int buildIndex)
        {
            var sceneStartupWidgetPrefabs = Settings.GetSceneStartupWidgetPrefabs(buildIndex);
            foreach (var sceneStartupWidgetPrefab in sceneStartupWidgetPrefabs)
            {
                UIManagerInterface.ShowWidget(sceneStartupWidgetPrefab);
                var sceneStartupWidget = UIManagerInterface.GetWidgetInstance(sceneStartupWidgetPrefab);
                m_SceneStartupWidgets.Add(sceneStartupWidget);
            }
        }
    }
}
