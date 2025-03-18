using UnityEngine;

namespace Eu4ng.Framework.OutGame
{
    /// <summary>
    /// 기본 위젯 클래스로 UIManager 클래스와 연동되어 있습니다.
    /// </summary>
    public abstract class UserWidget : MonoBehaviour, IUserWidget, IUIManager, IModalManager
    {
        RectTransform m_Prefab;

        /* UserWidget */

        protected virtual IUIManager UIManagerInterface => UIManager.Instance;
        protected virtual IModalManager ModalManagerInterface => UIManager.Instance;

        protected virtual void Refresh() { }

        /* IUserWidget */

        public RectTransform Prefab
        {
            get => m_Prefab;
            set => m_Prefab ??= value;
        }

        public void Show() => ShowWidget(Prefab);

        public void Hide() => HideWidget(Prefab);

        public void Remove() => RemoveWidget(Prefab);

        [field: SerializeField]
        public bool IsGlobalWidget { get; set; }

        /* IUIManager */

        public RectTransform GetWidget(RectTransform widgetPrefab) => UIManagerInterface.GetWidget(widgetPrefab);

        public void ShowWidget(RectTransform widgetPrefab) => UIManagerInterface.ShowWidget(widgetPrefab);

        public void HideWidget(RectTransform widgetPrefab) => UIManagerInterface.HideWidget(widgetPrefab);

        public void RemoveWidget(RectTransform widgetPrefab) => UIManagerInterface.RemoveWidget(widgetPrefab);

        /* IModalManager */

        public void RequestAlert(in ModalRequestData requestData, RectTransform widgetPrefab = null) => ModalManagerInterface.RequestAlert(requestData, widgetPrefab);

        public void RequestConfirm(in ModalRequestData requestData, RectTransform widgetPrefab = null) => ModalManagerInterface.RequestConfirm(requestData, widgetPrefab);

        public void RequestPrompt(in ModalRequestData requestData, RectTransform widgetPrefab = null) => ModalManagerInterface.RequestPrompt(requestData, widgetPrefab);

        /* MonoBehaviour */
        protected virtual void Awake() {}

        protected virtual void OnEnable() {}

        protected virtual void Start() {}

        protected virtual void FixedUpdate() {}

        protected virtual void Update() {}

        protected virtual void LateUpdate() {}

        protected virtual void OnDisable() {}

        protected virtual void OnDestroy() {}
    }
}
