using Eu4ng.Utilities;
using UnityEngine;

namespace Eu4ng.Framework.OutGame
{
    public class WidgetInstance : MonoBehaviour
    {
        [SerializeField, ReadOnly] RectTransform m_WidgetPrefab;

        public RectTransform WidgetPrefab
        {
            get => m_WidgetPrefab;
            set
            {
                if (value is null) return;
                if (m_WidgetPrefab is not null) return;
                m_WidgetPrefab = value;
            }
        }
        [field: SerializeField, ReadOnly] public bool IsGlobalWidget { get; set; }

        public void Show() => UIManager.Instance.ShowWidget(WidgetPrefab);
        public void Hide() => UIManager.Instance.HideWidget(WidgetPrefab);
    }
}
