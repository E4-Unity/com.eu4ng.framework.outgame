using System.Collections.Generic;
using UnityEngine;

namespace Eu4ng.Framework.OutGame
{
    public class SceneWidgetSettings : MonoBehaviour
    {
        [field: SerializeField] protected List<RectTransform> StartupWidgetPrefabs { get; private set; }

        protected virtual void Start()
        {
            ShowStartupWidgets();
        }

        protected virtual void ShowStartupWidgets()
        {
            foreach (var widgetPrefab in StartupWidgetPrefabs)
            {
                UIManager.Instance.ShowWidget(widgetPrefab);
            }
        }
    }
}
