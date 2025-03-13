using UnityEngine;

namespace Eu4ng.Framework.OutGame
{
    /// <summary>
    /// 기본 위젯 클래스로 UIManager 클래스와 연동되어 있습니다.
    /// </summary>
    public abstract class UserWidget : MonoBehaviour, IUserWidget, IUIManager
    {
        private RectTransform m_Prefab;

        /* UserWidget */

        protected virtual IUIManager UIManagerInterface => UIManager.Instance;

        /* IUserWidget */

        public RectTransform Prefab
        {
            get => m_Prefab;
            set
            {
                if(m_Prefab == null) m_Prefab = value;
            }
        }

        public void Show() => ShowWidget(Prefab);

        public void Hide() => HideWidget(Prefab);

        /* IUIManager */

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
