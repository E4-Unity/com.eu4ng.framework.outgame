using Eu4ng.Manager.Singleton;
using UnityEngine;

namespace Eu4ng.Framework.OutGame
{
    /// <summary>
    /// OutGameFramework 패키지 전용 설정
    /// </summary>
    public class OutGameFrameworkSettings : DeveloperSettings<OutGameFrameworkSettings>
    {
        /* Fields */

        [Header("Config")]
        [SerializeField] RectTransform m_OptionsWidget;

        /* Properties */

        public RectTransform OptionsWidget => m_OptionsWidget;

        /* DeveloperSettings */

        protected override void OnInitialize() {}
    }
}
