using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Eu4ng.Framework.OutGame
{
    /// <summary>
    /// UI Manager 클래스의 컴포넌트 클래스입니다.
    /// 동적 UI 생성 및 파괴를 담당합니다.
    /// </summary>
    public class DynamicWidgetManager : MonoBehaviour, IUIManager
    {
        /* Fields */

        [Header("References")]
        [SerializeField] Canvas m_Canvas;

        /* Properties */

        protected Dictionary<RectTransform, RectTransform> WidgetDictionary { get; private set; } = new Dictionary<RectTransform, RectTransform>();
        protected Transform CanvasTransform => m_Canvas?.transform;

        /* MonoBehaviour */

        protected virtual void Awake()
        {
            if(m_Canvas == null) m_Canvas = GetComponentInChildren<Canvas>();

            SceneManager.activeSceneChanged += OnActiveSceneChanged;
        }

        /* IUIManager */

        public RectTransform GetWidget(RectTransform widgetPrefab)
        {
            WidgetDictionary.TryGetValue(widgetPrefab, out var widgetInstance);

            return widgetInstance;
        }

        public virtual void ShowWidget(RectTransform widgetPrefab)
        {
            // 유효성 검사
            if (widgetPrefab == null) return;

            // 위젯 인스턴스 생성 혹은 가져오기
            RectTransform widgetInstance = WidgetDictionary.TryGetValue(widgetPrefab, out var cachedWidgetInstance) ? cachedWidgetInstance : AddWidget(widgetPrefab);
            if (widgetInstance == null) return;

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
            if (!WidgetDictionary.TryGetValue(widgetPrefab, out var widgetInstance))
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

        public void ToggleWidget(RectTransform widgetPrefab)
        {
            // 유효성 검사
            if (widgetPrefab == null) return;

            if (!WidgetDictionary.TryGetValue(widgetPrefab, out var widgetInstance) || !widgetInstance.gameObject.activeSelf)
            {
                ShowWidget(widgetPrefab);
            }
            else
            {
                HideWidget(widgetPrefab);
            }
        }

        public virtual void RemoveWidget(RectTransform widgetPrefab)
        {
            // 유효성 검사
            if (widgetPrefab == null) return;

            // 등록 여부 확인
            if (!WidgetDictionary.TryGetValue(widgetPrefab, out var widgetInstance)) return;

            // 위젯 인스턴스 파괴 및 등록 해제
            Destroy(widgetInstance.gameObject);
            WidgetDictionary.Remove(widgetPrefab);

            LogOutGameFramework.Log("Remove widget(" + widgetPrefab.gameObject.name + ")");
        }

        /* UIManager */

        protected virtual RectTransform AddWidget(RectTransform widgetPrefab)
        {
            // 유효성 검사
            if (!CanvasTransform) return null;
            if (widgetPrefab == null) return null;

            // 인터페이스 검사
            if (widgetPrefab.GetComponent<IUserWidget>() == null)
            {
                LogOutGameFramework.LogError(widgetPrefab.name + " should implement IUserWidget");
                return null;
            }

            // 중복 검사
            if (WidgetDictionary.TryGetValue(widgetPrefab, out var widgetInstance))
            {
                LogOutGameFramework.LogWarning("Widget(" + widgetPrefab.gameObject.name + ") is already added.");
                return widgetInstance;
            }
            else
            {
                // 위젯 인스턴스 생성 및 등록
                widgetInstance = CreateWidgetInstance(widgetPrefab);
                WidgetDictionary.Add(widgetPrefab, widgetInstance);

                LogOutGameFramework.Log("Add widget(" + widgetPrefab.gameObject.name + ")");
                return widgetInstance;
            }
        }

        protected virtual RectTransform CreateWidgetInstance(RectTransform widgetPrefab)
        {
            // 위젯 인스턴스 생성
            bool cachedActiveSelf = widgetPrefab.gameObject.activeSelf;
            widgetPrefab.gameObject.SetActive(false);
            RectTransform widgetInstance = Instantiate(widgetPrefab, CanvasTransform);
            widgetPrefab.gameObject.SetActive(cachedActiveSelf);

            // 위젯 인스턴스 초기화
            IUserWidget userWidget = widgetInstance.GetComponent<IUserWidget>();
            userWidget.Prefab = widgetPrefab;

            return widgetInstance;
        }

        protected virtual void RemoveAllWidgets()
        {
            List<RectTransform> widgetPrefabs = new List<RectTransform>(WidgetDictionary.Keys);
            foreach (var widgetPrefab in widgetPrefabs)
            {
                RemoveWidget(widgetPrefab);
            }
        }

        protected virtual void OnActiveSceneChanged(Scene currentScene, Scene nextScene)
        {
            DestroySceneWidgets();
        }

        protected virtual void DestroySceneWidgets()
        {
            // GlobalWidget으로 설정된 위젯들을 제외한 모든 위젯 가져오기
            List<RectTransform> widgetPrefabsToDestroy = new List<RectTransform>(WidgetDictionary.Count);
            foreach (var pair in WidgetDictionary)
            {
                var widgetPrefab = pair.Key;
                var widget = pair.Value;

                IUserWidget userWidget = widget.GetComponent<IUserWidget>();
                if(userWidget.IsGlobalWidget) continue;

                widgetPrefabsToDestroy.Add(widgetPrefab);
            }

            // GlobalWidget으로 설정된 위젯들을 제외한 모든 위젯 파괴 및 제거
            foreach (var widgetPrefabToDestroy in widgetPrefabsToDestroy)
            {
                RemoveWidget(widgetPrefabToDestroy);
            }
        }
    }
}
