using UnityEngine;

namespace Eu4ng.Framework.OutGame
{
    /// <summary>
    /// 모든 씬에서 사용할 수 있는 공용 UIManager 싱글톤 클래스입니다.
    /// Alert, Confirm, Prompt 등의 모달 창을 띄울 수 있습니다.
    /// </summary>
    public class GlobalUIManager : UIManagerBase<GlobalUIManager>, IModalManager
    {
        [SerializeField] RectTransform m_AlertWidgetPrefab;
        [SerializeField] RectTransform m_ConfirmWidgetPrefab;
        [SerializeField] RectTransform m_PromptWidgetPrefab;

        /* GlobalUIManager */

        protected virtual void RequestModal(in ModalRequestData requestData, RectTransform widgetPrefab = null)
        {
            if (widgetPrefab == null || widgetPrefab.GetComponent<IModalWidget>() == null) return;

            var widgetInstance = AddWidget(widgetPrefab);
            IModalWidget modalWidget = widgetInstance.GetComponent<IModalWidget>();
            modalWidget.RequestData = requestData;
            ShowWidget(widgetPrefab);
        }

        /* IModalManager */

        public void RequestAlert(in ModalRequestData requestData, RectTransform widgetPrefab = null)
        {
            widgetPrefab ??= m_AlertWidgetPrefab;
            RequestModal(requestData, widgetPrefab);
        }

        public void RequestConfirm(in ModalRequestData requestData, RectTransform widgetPrefab = null)
        {
            widgetPrefab ??= m_ConfirmWidgetPrefab;
            RequestModal(requestData, widgetPrefab);
        }

        public void RequestPrompt(in ModalRequestData requestData, RectTransform widgetPrefab = null)
        {
            widgetPrefab ??= m_PromptWidgetPrefab;
            RequestModal(requestData, widgetPrefab);
        }
    }
}
