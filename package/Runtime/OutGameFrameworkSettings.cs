using System;
using Eu4ng.Manager.Singleton;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Eu4ng.Framework.OutGame
{
    [Serializable]
    public class OtherSettings
    {
        public static OtherSettings Instance => OutGameFrameworkSettings.Instance.OtherSettings;

        [field: SerializeField] public RectTransform OptionsWidget { get; private set; }
    }

    /// <summary>
    /// OutGameFramework 패키지 전용 설정
    /// </summary>
    public class OutGameFrameworkSettings : DeveloperSettings<OutGameFrameworkSettings>
    {
        /* Fields */

        [field: SerializeField] public UIManagerSettings UIManagerSettings { get; private set; }
        [field: SerializeField] public SceneLoadingManagerSettings SceneLoadingManagerSettings { get; private set; }
        [field: SerializeField] public OtherSettings OtherSettings { get; private set; }

        /* DeveloperSettings */

        protected override void OnInitialize() {}
    }
}
