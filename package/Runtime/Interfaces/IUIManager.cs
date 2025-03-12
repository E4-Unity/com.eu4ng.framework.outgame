using UnityEngine;

namespace Eu4ng.Framework.OutGame
{
    public interface IUIManager
    {
        public void ShowWidget(RectTransform widgetPrefab);

        public void HideWidget(RectTransform widgetPrefab);
    }
}
