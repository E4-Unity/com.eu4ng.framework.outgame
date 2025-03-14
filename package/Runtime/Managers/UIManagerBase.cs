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

        public RectTransform GetWidgetInstance(RectTransform widgetPrefab)
        {
            m_WidgetDictionary.TryGetValue(widgetPrefab, out var widgetInstance);

            return widgetInstance;
        }

        public virtual void ShowWidget(RectTransform widgetPrefab)
        {
            // 유효성 검사
            if (widgetPrefab == null) return;

            // 위젯 인스턴스 생성 혹은 가져오기
            RectTransform widgetInstance = m_WidgetDictionary.TryGetValue(widgetPrefab, out var cachedWidgetInstance) ? cachedWidgetInstance : AddWidget(widgetPrefab);

            // 표시 여부 확인
            if (!widgetInstance.gameObject.activeSelf)
            {
                LogOutGameFramework.Log("Show Widget(" + widgetPrefab.gameObject.name + ")");
                widgetInstance.gameObject.SetActive(true);
            }
            else
            {
                LogOutGameFramework.LogWarning("Widget(" + widgetPrefab.gameObject.name + ") is already visible.");
            }
        }

        public virtual void HideWidget(RectTransform widgetPrefab)
        {
            // 유효성 검사
            if (widgetPrefab == null) return;

            // 등록 여부 확인
            if (!m_WidgetDictionary.TryGetValue(widgetPrefab, out var widgetInstance))
            {
                LogOutGameFramework.Log("Widget(" + widgetPrefab.gameObject.name + ") is not added.");
                return;
            }

            // 표시 여부 확인
            if (widgetInstance.gameObject.activeSelf)
            {
                LogOutGameFramework.Log("Hide Widget(" + widgetPrefab.gameObject.name + ")");
                widgetInstance.gameObject.SetActive(false);
            }
            else
            {
                LogOutGameFramework.LogWarning("Widget(" + widgetPrefab.gameObject.name + ") is already invisible.");
            }
        }

        /* UIManagerBase */

        protected virtual RectTransform AddWidget(RectTransform widgetPrefab)
        {
            // 유효성 검사
            if (widgetPrefab == null || widgetPrefab.GetComponent<IUserWidget>() == null) return null;
            if (!m_Canvas) return null;

            // 중복 검사
            if (m_WidgetDictionary.TryGetValue(widgetPrefab, out var widgetInstance))
            {
                LogOutGameFramework.LogWarning("Widget(" + widgetPrefab.gameObject.name + ") is already added.");
                return widgetInstance;
            }
            else
            {
                // 위젯 인스턴스 생성 및 등록
                widgetInstance = CreateWidgetInstance(widgetPrefab);
                m_WidgetDictionary.Add(widgetPrefab, widgetInstance);

                LogOutGameFramework.Log("Add widget(" + widgetPrefab.gameObject.name + ")");
                return widgetInstance;
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

            LogOutGameFramework.Log("Remove widget(" + widgetPrefab.gameObject.name + ")");

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
