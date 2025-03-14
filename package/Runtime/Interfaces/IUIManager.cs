using UnityEngine;

namespace Eu4ng.Framework.OutGame
{
    public interface IUIManager
    {
        public RectTransform GetWidgetInstance(RectTransform widgetPrefab);

        public void ShowWidget(RectTransform widgetPrefab);

        public void HideWidget(RectTransform widgetPrefab);
    }
}
