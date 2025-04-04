using Eu4ng.Utilities;
using UnityEngine;

namespace Eu4ng.Framework.OutGame
{
    public class WidgetInstance : MonoBehaviour
    {
        [field: SerializeField, ReadOnly] public RectTransform WidgetPrefab { get; private set; }
        [field: SerializeField, ReadOnly] public bool IsGlobalWidget { get; private set; }

        public void Initialize(RectTransform widgetPrefab, bool isGlobalWidget)
        {
            if (widgetPrefab is null) return;
            if (WidgetPrefab is not null) return;

            WidgetPrefab = widgetPrefab;
            IsGlobalWidget = isGlobalWidget;
        }
        public void Show() => UIManager.Instance.ShowWidget(WidgetPrefab);
        public void Hide() => UIManager.Instance.HideWidget(WidgetPrefab);
    }
}
