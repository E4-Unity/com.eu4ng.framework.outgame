using UnityEngine;

namespace Eu4ng.Framework.OutGame
{
    /// <summary>
    /// 클릭 시 특정 위젯을 토글하는 버튼
    /// </summary>
    public class WidgetButton : ButtonWidget
    {
        [SerializeField] RectTransform m_WidgetPrefab;

        protected RectTransform WidgetPrefab => m_WidgetPrefab;

        protected override void OnButtonClicked()
        {
            base.OnButtonClicked();

            if (WidgetPrefab)
            {
                ToggleWidget(WidgetPrefab);
            }
        }
    }
}
