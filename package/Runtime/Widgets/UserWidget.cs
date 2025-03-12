using UnityEngine;

namespace Eu4ng.Framework.OutGame
{
    /// <summary>
    /// 기본 위젯 클래스로 UIManager 클래스와 연동되어 있습니다.
    /// </summary>
    public abstract class UserWidget : MonoBehaviour, IUIManager
    {
        /* UserWidget */

        protected virtual IUIManager UIManagerInterface => UIManager.Instance;

        /* IUIManager */

        public void AddWidget(RectTransform widgetPrefab) => UIManagerInterface.AddWidget(widgetPrefab);

        public void RemoveWidget(RectTransform widgetPrefab) => UIManagerInterface.RemoveWidget(widgetPrefab);

        public void RemoveAllWidgets() => UIManagerInterface.RemoveAllWidgets();

        public void ShowWidget(RectTransform widgetPrefab) => UIManagerInterface.ShowWidget(widgetPrefab);

        public void HideWidget(RectTransform widgetPrefab) => UIManagerInterface.HideWidget(widgetPrefab);

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
