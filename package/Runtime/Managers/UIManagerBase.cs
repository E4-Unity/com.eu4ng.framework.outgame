using System.Collections.Generic;
using UnityEngine;

using Eu4ng.Manager.Singleton;
using Eu4ng.Utilities;

namespace Eu4ng.Framework.OutGame
{
    [RequireComponent(typeof(DynamicWidgetManager))]
    public abstract class UIManagerBase<T> : MonoSingleton<T>, IUIManager where T : UIManagerBase<T>
    {
        /* Components */

        [SerializeField, ReadOnly] DynamicWidgetManager m_DynamicWidgetManager;

        IUIManager UIManagerInterface => m_DynamicWidgetManager;

        /* Fields */

        [SerializeField]
        protected List<RectTransform> m_StartupWidgetPrefabs = new List<RectTransform>();

        /* IUIManager */

        public RectTransform GetWidgetInstance(RectTransform widgetPrefab) => UIManagerInterface.GetWidgetInstance(widgetPrefab);

        public void ShowWidget(RectTransform widgetPrefab) => UIManagerInterface.ShowWidget(widgetPrefab);

        public void HideWidget(RectTransform widgetPrefab) => UIManagerInterface.HideWidget(widgetPrefab);

        public void RemoveWidget(RectTransform widgetPrefab) => UIManagerInterface.RemoveWidget(widgetPrefab);

        /* MonoSingleton */

        protected override void OnInitialize()
        {
            m_DynamicWidgetManager = GetComponent<DynamicWidgetManager>();
        }

        /* MonoBehaviour */

        protected override void Start()
        {
            base.Start();

            foreach (var widgetPrefab in m_StartupWidgetPrefabs)
            {
                ShowWidget(widgetPrefab);
            }
        }
    }
}
