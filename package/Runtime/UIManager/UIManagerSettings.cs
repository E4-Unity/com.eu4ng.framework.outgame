using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Eu4ng.Framework.OutGame
{
    [Serializable]
    public class UIManagerSettings
    {
        public static UIManagerSettings Instance => OutGameFrameworkSettings.Instance.UIManagerSettings;

        [field: Header("Canvas")]
        [field: SerializeField] public Canvas CanvasPrefab { get; private set; }
        [field: SerializeField] public EventSystem EventSystemPrefab { get; private set; }

        [field: Header("Modal")]
        [field: SerializeField] public RectTransform AlertWidgetPrefab { get; private set; }
        [field: SerializeField] public RectTransform ConfirmWidgetPrefab { get; private set; }
        [field: SerializeField] public RectTransform PromptWidgetPrefab { get; private set; }
    }
}
