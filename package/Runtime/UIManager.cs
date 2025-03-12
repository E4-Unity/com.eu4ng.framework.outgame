using System.Collections.Generic;
using UnityEngine;

using Eu4ng.Manager.Singleton;

namespace Eu4ng.Framework.OutGame
{
    public class UIManager : MonoSingleton<UIManager>, IUIManager
    {
        [SerializeField]
        protected Canvas m_Canvas;

        [SerializeField]
        protected List<RectTransform> m_StartupWidgetPrefabs = new List<RectTransform>();

        protected Dictionary<RectTransform, RectTransform> m_StartupWidgetDictionary =
            new Dictionary<RectTransform, RectTransform>();

        /* IUIManager */

        public virtual void AddWidget(RectTransform widgetPrefab)
        {
            // 유효성 검사
            if (widgetPrefab == null) return;
            if (!m_Canvas) return;

            RectTransform widgetInstance = Instantiate(widgetPrefab, m_Canvas.transform);
            m_StartupWidgetDictionary.Add(widgetPrefab, widgetInstance);

            Debug.Log("Add widget(" + widgetPrefab.gameObject.name + ")");
        }

        public virtual void RemoveWidget(RectTransform widgetPrefab)
        {
            // 유효성 검사
            if (widgetPrefab == null) return;
            if (!m_StartupWidgetDictionary.TryGetValue(widgetPrefab, out var widgetInstance)) return;

            Destroy(widgetInstance.gameObject);
            m_StartupWidgetDictionary.Remove(widgetPrefab);

            Debug.Log("Remove widget(" + widgetPrefab.gameObject.name + ")");
        }

        public virtual void RemoveAllWidgets()
        {
            List<RectTransform> widgetPrefabs = new List<RectTransform>(m_StartupWidgetDictionary.Keys);
            foreach (var widgetPrefab in widgetPrefabs)
            {
                RemoveWidget(widgetPrefab);
            }
        }

        public virtual void ShowWidget(RectTransform widgetPrefab)
        {
            // 유효성 검사
            if (widgetPrefab == null) return;
            if (!m_StartupWidgetDictionary.TryGetValue(widgetPrefab, out var widgetInstance)) return;

            if (!widgetInstance.gameObject.activeSelf)
            {
                Debug.Log("Show Widget(" + widgetPrefab.gameObject.name + ")");
                widgetInstance.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogWarning("Widget(" + widgetPrefab.gameObject.name + ") is already visible.");
            }
        }

        public virtual void HideWidget(RectTransform widgetPrefab)
        {
            // 유효성 검사
            if (widgetPrefab == null) return;
            if (!m_StartupWidgetDictionary.TryGetValue(widgetPrefab, out var widgetInstance)) return;

            if (widgetInstance.gameObject.activeSelf)
            {
                Debug.Log("Hide Widget(" + widgetPrefab.gameObject.name + ")");
                widgetInstance.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogWarning("Widget(" + widgetPrefab.gameObject.name + ") is already invisible.");
            }
        }

        /* MonoBehaviour */

        protected override void Awake()
        {
            base.Awake();

            if (m_Canvas == null)
            {
                m_Canvas = GetComponent<Canvas>();
            }
        }

        protected override void Start()
        {
            base.Start();

            foreach (var widgetPrefab in m_StartupWidgetPrefabs)
            {
                AddWidget(widgetPrefab);
            }
        }
    }
}
