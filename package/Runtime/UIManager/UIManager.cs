using UnityEngine;

using Eu4ng.Manager.Singleton;
using Eu4ng.Utilities;
using UnityEngine.EventSystems;

namespace Eu4ng.Framework.OutGame
{
    [RequireComponent(typeof(DynamicWidgetManager), typeof(ModalManager))]
    public class UIManager : GameSubsystem<UIManager>, IUIManager, IModalManager
    {
        [field: Header("Components")]
        [field: SerializeField, ReadOnly] public DynamicWidgetManager DynamicWidgetManager { get; private set; }
        [field: SerializeField, ReadOnly] public ModalManager ModalManager { get; private set; }

        [field: Header("UI")]
        [field: SerializeField, ReadOnly] public Canvas GlobalCanvas { get; private set; }
        [field: SerializeField, ReadOnly] public EventSystem GlobalEventSystem { get; private set; }

        IUIManager UIManagerInterface => DynamicWidgetManager;
        IModalManager ModalManagerInterface => ModalManager;

        /* IUIManager */

        public RectTransform GetWidget(RectTransform widgetPrefab) => UIManagerInterface.GetWidget(widgetPrefab);

        public void ShowWidget(RectTransform widgetPrefab) => UIManagerInterface.ShowWidget(widgetPrefab);

        public void HideWidget(RectTransform widgetPrefab) => UIManagerInterface.HideWidget(widgetPrefab);

        public void ToggleWidget(RectTransform widgetPrefab) => UIManagerInterface.ToggleWidget(widgetPrefab);

        public void RemoveWidget(RectTransform widgetPrefab) => UIManagerInterface.RemoveWidget(widgetPrefab);

        /* IModalManager */

        public void RequestAlert(in ModalRequestData requestData, RectTransform widgetPrefab = null) => ModalManagerInterface.RequestAlert(requestData, widgetPrefab);

        public void RequestConfirm(in ModalRequestData requestData, RectTransform widgetPrefab = null) => ModalManagerInterface.RequestConfirm(requestData, widgetPrefab);

        public void RequestPrompt(in ModalRequestData requestData, RectTransform widgetPrefab = null) => ModalManagerInterface.RequestPrompt(requestData, widgetPrefab);

        /* MonoSingleton */

        protected override void OnInitialize()
        {
            SpawnCanvas();

            DynamicWidgetManager = GetComponent<DynamicWidgetManager>();
            DynamicWidgetManager.GlobalCanvas = GlobalCanvas;
            ModalManager = GetComponent<ModalManager>();
        }

        /* UIManager */

        void SpawnCanvas()
        {
            var settings = UIManagerSettings.Instance;
            GlobalCanvas = Instantiate(settings.CanvasPrefab, transform);
            GlobalEventSystem = Instantiate(settings.EventSystemPrefab, transform);
        }
    }
}
