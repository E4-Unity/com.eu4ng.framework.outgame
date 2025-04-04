using Eu4ng.Utilities;
using UnityEngine;

namespace Eu4ng.Framework.OutGame
{
    /// <summary>
    /// 위젯 닫기 버튼
    /// </summary>
    public class CloseButton : ButtonWidget
    {
        /* SerializeFields */

        [SerializeField, ReadOnly] WidgetInstance m_TargetWidgetInstanceComponent;

        /* SerializeProperties */

        [field: SerializeField] protected RectTransform TargetWidget { get; private set; }

        /* Properties */

        protected WidgetInstance TargetWidgetInstanceComponent =>
            m_TargetWidgetInstanceComponent ?? TargetWidget?.GetComponent<WidgetInstance>();

        /* CloseButton */
        
        protected override void OnButtonClicked()
        {
            base.OnButtonClicked();

            if (TargetWidget == null) return;

            if (TargetWidgetInstanceComponent is not null)
            {
                TargetWidgetInstanceComponent.Hide();
            }
            else
            {
                TargetWidget.gameObject.SetActive(false);
            }
        }
    }
}
