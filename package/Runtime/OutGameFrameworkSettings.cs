using System.Collections.Generic;
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

        [field: SerializeField] public UIManagerSettings UIManagerSettings { get; private set; }

        [Header("Config")]
        [SerializeField] RectTransform m_OptionsWidget;

        [Header("Modal Manager")]
        [SerializeField] RectTransform m_AlertWidgetPrefab;
        [SerializeField] RectTransform m_ConfirmWidgetPrefab;
        [SerializeField] RectTransform m_PromptWidgetPrefab;

        [Header("Scene Widgets Manager")]
        [SerializeField] List<RectTransform> m_GlobalStartupWidgetPrefabs;

        [Header("Scene Loading Manager")]
        [SerializeField] RectTransform m_LoadingWidgetPrefab;

        /* Properties */

        public RectTransform OptionsWidget => m_OptionsWidget;

        // Modal Manager
        public RectTransform AlertWidgetPrefab => m_AlertWidgetPrefab;
        public RectTransform ConfirmWidgetPrefab => m_ConfirmWidgetPrefab;
        public RectTransform PromptWidgetPrefab => m_PromptWidgetPrefab;

        // Scene Widgets Manager
        public List<RectTransform> GlobalStartupWidgetPrefabs => m_GlobalStartupWidgetPrefabs;

        // Scene Loading Manager
        public RectTransform LoadingWidgetPrefab => m_LoadingWidgetPrefab;

        /* DeveloperSettings */

        protected override void OnInitialize() {}
    }
}
