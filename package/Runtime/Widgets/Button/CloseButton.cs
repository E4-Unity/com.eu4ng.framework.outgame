using UnityEngine;

namespace Eu4ng.Framework.OutGame
{
    /// <summary>
    /// 위젯 닫기 버튼
    /// </summary>
    public class CloseButton : ButtonWidget
    {
        [SerializeField] RectTransform m_RootWidget;

        protected RectTransform WidgetPrefab => m_RootWidget == null ? null : m_RootWidget.GetComponent<IUserWidget>()?.Prefab;

        protected override void OnButtonClicked()
        {
            base.OnButtonClicked();

            if (WidgetPrefab != null) HideWidget(WidgetPrefab);
        }
    }
}
