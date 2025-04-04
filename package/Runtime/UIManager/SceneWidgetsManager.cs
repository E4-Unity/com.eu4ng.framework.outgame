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

        OutGameFrameworkSettings Settings => OutGameFrameworkSettings.Instance;
        IUIManager UIManagerInterface => UIManager.Instance;

        protected virtual void Awake()
        {
            CreateGlobalWidgets();
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
                var widgetInstance = globalStartupWidget.GetComponent<WidgetInstance>();
                if (!widgetInstance.IsGlobalWidget)
                {
                    widgetInstance.IsGlobalWidget = true;

                    LogOutGameFramework.LogWarning(globalStartupWidgetPrefab.name + " is not set as global widget. Force set widgetInstance as global widget.");
                }
            }
        }
    }
}
