using System.Collections.Generic;
using UnityEngine;

using Eu4ng.Manager.Singleton;
using Eu4ng.Utilities;

namespace Eu4ng.Framework.OutGame
{
    [RequireComponent(typeof(DynamicWidgetManager), typeof(ModalManager), typeof(SceneWidgetsManager))]
    public abstract class UIManager : MonoSingleton<UIManager>, IUIManager, IModalManager
    {
        [Header("Components")]
        [SerializeField, ReadOnly] DynamicWidgetManager m_DynamicWidgetManager;
        [SerializeField, ReadOnly] ModalManager m_ModalManager;
        [SerializeField, ReadOnly] SceneWidgetsManager m_SceneWidgetsManager;

        IUIManager UIManagerInterface => m_DynamicWidgetManager;
        IModalManager ModalManagerInterface => m_ModalManager;

        /* Fields */

        [SerializeField]
        protected List<RectTransform> m_StartupWidgetPrefabs = new List<RectTransform>();

        /* IUIManager */

        public RectTransform GetWidgetInstance(RectTransform widgetPrefab) => UIManagerInterface.GetWidgetInstance(widgetPrefab);

        public void ShowWidget(RectTransform widgetPrefab) => UIManagerInterface.ShowWidget(widgetPrefab);

        public void HideWidget(RectTransform widgetPrefab) => UIManagerInterface.HideWidget(widgetPrefab);

        public void RemoveWidget(RectTransform widgetPrefab) => UIManagerInterface.RemoveWidget(widgetPrefab);

        /* IModalManager */

        public void RequestAlert(in ModalRequestData requestData, RectTransform widgetPrefab = null) => ModalManagerInterface.RequestAlert(requestData, widgetPrefab);

        public void RequestConfirm(in ModalRequestData requestData, RectTransform widgetPrefab = null) => ModalManagerInterface.RequestConfirm(requestData, widgetPrefab);

        public void RequestPrompt(in ModalRequestData requestData, RectTransform widgetPrefab = null) => ModalManagerInterface.RequestPrompt(requestData, widgetPrefab);

        /* MonoSingleton */

        protected override void OnInitialize()
        {
            m_DynamicWidgetManager = GetComponent<DynamicWidgetManager>();
            m_ModalManager = GetComponent<ModalManager>();
            m_SceneWidgetsManager = GetComponent<SceneWidgetsManager>();
        }

        /* MonoBehaviour */

        protected override void Start()
        {
            base.Start();

            foreach (var widgetPrefab in m_StartupWidgetPrefabs)
            {
                ShowWidget(widgetPrefab);
            }
        }
    }
}
