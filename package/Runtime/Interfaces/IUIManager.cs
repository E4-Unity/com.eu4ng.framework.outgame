using UnityEngine;

namespace Eu4ng.Framework.OutGame
{
    public interface IUIManager
    {
        public RectTransform GetWidget(RectTransform widgetPrefab);

        public void ShowWidget(RectTransform widgetPrefab);

        public void HideWidget(RectTransform widgetPrefab);

        public void RemoveWidget(RectTransform widgetPrefab);
    }
}
