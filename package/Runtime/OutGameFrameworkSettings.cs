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

        [Header("Config")]
        [SerializeField] RectTransform m_OptionsWidget;

        [Header("Modal Manager")]
        [SerializeField] RectTransform m_AlertWidgetPrefab;
        [SerializeField] RectTransform m_ConfirmWidgetPrefab;
        [SerializeField] RectTransform m_PromptWidgetPrefab;

        [Header("Scene Widgets Manager")]
        [SerializeField] List<RectTransform> m_GlobalWidgetPrefabs;
        [SerializeReference] List<SceneWidgetsConfig> m_SceneWidgetsConfigList;

        readonly Dictionary<int, List<RectTransform>> m_SceneWidgetsDictionary = new Dictionary<int, List<RectTransform>>();

        /* Properties */

        public RectTransform OptionsWidget => m_OptionsWidget;

        // Modal Manager
        public RectTransform AlertWidgetPrefab => m_AlertWidgetPrefab;
        public RectTransform ConfirmWidgetPrefab => m_ConfirmWidgetPrefab;
        public RectTransform PromptWidgetPrefab => m_PromptWidgetPrefab;

        // Scene Widgets Manager
        public List<RectTransform> GlobalWidgetPrefabs => m_GlobalWidgetPrefabs;
        public List<RectTransform> GetStartupWidgets(int buildIndex) => m_SceneWidgetsDictionary.GetValueOrDefault(buildIndex, new List<RectTransform>());

        /* DeveloperSettings */

        protected override void OnInitialize()
        {
            foreach (var sceneWidgetsConfig in m_SceneWidgetsConfigList)
            {
                if (sceneWidgetsConfig != null)
                {
                    m_SceneWidgetsDictionary.TryAdd(sceneWidgetsConfig.BuildIndex, sceneWidgetsConfig.StartupWidgets);
                }
            }
        }
    }
}
