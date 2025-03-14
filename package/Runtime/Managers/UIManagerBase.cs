using System.Collections.Generic;
using UnityEngine;

using Eu4ng.Manager.Singleton;

namespace Eu4ng.Framework.OutGame
{
    public abstract class UIManagerBase<T> : MonoSingleton<T>, IUIManager where T : UIManagerBase<T>
    {
        [SerializeField]
        protected Canvas m_Canvas;

        [SerializeField]
        protected List<RectTransform> m_StartupWidgetPrefabs = new List<RectTransform>();

        protected Dictionary<RectTransform, RectTransform> m_WidgetDictionary =
            new Dictionary<RectTransform, RectTransform>();

        /* IUIManager */

        public virtual void ShowWidget(RectTransform widgetPrefab)
        {
            // 유효성 검사
            if (widgetPrefab == null) return;

            // 등록되지 않은 경우 새로 등록
            if(!m_WidgetDictionary.ContainsKey(widgetPrefab)) AddWidget(widgetPrefab);

            // 등록 여부 확인
            if (!m_WidgetDictionary.TryGetValue(widgetPrefab, out var widgetInstance)) return;

            // 표시 여부 확인
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

            // 등록 여부 확인
            if (!m_WidgetDictionary.TryGetValue(widgetPrefab, out var widgetInstance)) return;

            // 표시 여부 확인
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

        /* UIManagerBase */

        protected virtual bool AddWidget(RectTransform widgetPrefab)
        {
            // 유효성 검사
            if (widgetPrefab == null || widgetPrefab.GetComponent<IUserWidget>() == null) return false;
            if (!m_Canvas) return false;

            // 중복 검사
            if (m_WidgetDictionary.ContainsKey(widgetPrefab))
            {
                Debug.LogWarning("Widget(" + widgetPrefab.gameObject.name + ") is already added.");
                return false;
            }
            else
            {
                // 위젯 인스턴스 생성 및 등록
                RectTransform widgetInstance = CreateWidgetInstance(widgetPrefab);
                m_WidgetDictionary.Add(widgetPrefab, widgetInstance);

                Debug.Log("Add widget(" + widgetPrefab.gameObject.name + ")");
                return true;
            }
        }

        protected virtual RectTransform CreateWidgetInstance(RectTransform widgetPrefab)
        {
            // 위젯 인스턴스 생성
            bool cachedActiveSelf = widgetPrefab.gameObject.activeSelf;
            widgetPrefab.gameObject.SetActive(false);
            RectTransform widgetInstance = Instantiate(widgetPrefab, m_Canvas.transform);
            widgetPrefab.gameObject.SetActive(cachedActiveSelf);

            // 위젯 인스턴스 초기화
            IUserWidget userWidget = widgetInstance.GetComponent<IUserWidget>();
            userWidget.Prefab = widgetPrefab;

            return widgetInstance;
        }

        protected virtual bool RemoveWidget(RectTransform widgetPrefab)
        {
            // 유효성 검사
            if (widgetPrefab == null) return false;

            // 등록 여부 확인
            if (!m_WidgetDictionary.TryGetValue(widgetPrefab, out var widgetInstance)) return false;

            // 위젯 인스턴스 파괴 및 등록 해제
            Destroy(widgetInstance.gameObject);
            m_WidgetDictionary.Remove(widgetPrefab);

            Debug.Log("Remove widget(" + widgetPrefab.gameObject.name + ")");

            return true;
        }

        protected virtual void RemoveAllWidgets()
        {
            List<RectTransform> widgetPrefabs = new List<RectTransform>(m_WidgetDictionary.Keys);
            foreach (var widgetPrefab in widgetPrefabs)
            {
                RemoveWidget(widgetPrefab);
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
                ShowWidget(widgetPrefab);
            }
        }
    }
}
