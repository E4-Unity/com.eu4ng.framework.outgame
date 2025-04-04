using System;
using UnityEngine;

namespace Eu4ng.Framework.OutGame
{
    [Serializable]
    public class SceneLoadingManagerSettings
    {
        public static SceneLoadingManagerSettings Instance => OutGameFrameworkSettings.Instance.SceneLoadingManagerSettings;

        [field: SerializeField] public RectTransform LoadingWidgetPrefab { get; private set; }
        [field: SerializeField] public float MinimumLoadingScreenDisplayTime { get; private set; } = 1.0f;
        [field: SerializeField] public float FadeTime { get; private set; } = 1.0f;
    }
}
