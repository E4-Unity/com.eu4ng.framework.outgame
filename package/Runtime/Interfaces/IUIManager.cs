using UnityEngine;

namespace Eu4ng.Framework.OutGame
{
    public interface IUIManager
    {
        public void AddWidget(RectTransform widgetPrefab);

        public void RemoveWidget(RectTransform widgetPrefab);

        public void RemoveAllWidgets();

        public void ShowWidget(RectTransform widgetPrefab);

        public void HideWidget(RectTransform widgetPrefab);
    }
}
