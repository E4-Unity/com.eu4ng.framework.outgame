using UnityEngine;

namespace Eu4ng.Framework.OutGame
{
    /// <summary>
    /// UI Manager 클래스의 컴포넌트 클래스입니다.
    /// Alert, Confirm, Prompt 등의 모달 창 관리를 담당합니다.
    /// </summary>
    public class ModalManager : MonoBehaviour, IModalManager
    {
        /* Properties */

        OutGameFrameworkSettings Settings => OutGameFrameworkSettings.Instance;
        IUIManager UIManagerInterface => UIManager.Instance;

        /* IModalManager */

        protected virtual void RequestModal(in ModalRequestData requestData, RectTransform widgetPrefab = null)
        {
            if (UIManagerInterface == null) return;
            if (widgetPrefab == null || widgetPrefab.GetComponent<IModalWidget>() == null) return;

            LogOutGameFramework.Log("Title: " + requestData.Title + ", Message: " + requestData.Message);

            UIManagerInterface.ShowWidget(widgetPrefab);

            IModalWidget modalWidgetInterface = UIManagerInterface.GetWidget(widgetPrefab).GetComponent<IModalWidget>();
            modalWidgetInterface.RequestData = requestData;
        }

        /* IModalManager */

        public virtual void RequestAlert(in ModalRequestData requestData, RectTransform widgetPrefab = null)
        {
            widgetPrefab ??= Settings.AlertWidgetPrefab;
            RequestModal(requestData, widgetPrefab);
        }

        public virtual void RequestConfirm(in ModalRequestData requestData, RectTransform widgetPrefab = null)
        {
            widgetPrefab ??= Settings.ConfirmWidgetPrefab;
            RequestModal(requestData, widgetPrefab);
        }

        public virtual void RequestPrompt(in ModalRequestData requestData, RectTransform widgetPrefab = null)
        {
            widgetPrefab ??= Settings.PromptWidgetPrefab;
            RequestModal(requestData, widgetPrefab);
        }
    }
}
