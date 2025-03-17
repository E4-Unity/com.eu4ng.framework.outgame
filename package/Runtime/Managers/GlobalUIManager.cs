using Eu4ng.Utilities;
using UnityEngine;

namespace Eu4ng.Framework.OutGame
{
    /// <summary>
    /// 모든 씬에서 사용할 수 있는 공용 UIManager 싱글톤 클래스입니다.
    /// Alert, Confirm, Prompt 등의 모달 창을 띄울 수 있습니다.
    /// </summary>
    [RequireComponent(typeof(ModalManager))]
    public class GlobalUIManager : UIManagerBase<GlobalUIManager>, IModalManager
    {
        [Header("Components")]
        [SerializeField, ReadOnly] ModalManager m_ModalManager;
        IModalManager ModalManagerInterface => m_ModalManager;

        /* MonoSingleton */

        protected override void OnInitialize()
        {
            base.OnInitialize();

            m_ModalManager = GetComponent<ModalManager>();
        }

        public void RequestAlert(in ModalRequestData requestData, RectTransform widgetPrefab = null) => ModalManagerInterface?.RequestAlert(requestData, widgetPrefab);

        public void RequestConfirm(in ModalRequestData requestData, RectTransform widgetPrefab = null) => ModalManagerInterface?.RequestConfirm(requestData, widgetPrefab);

        public void RequestPrompt(in ModalRequestData requestData, RectTransform widgetPrefab = null) => ModalManagerInterface?.RequestPrompt(requestData, widgetPrefab);
    }
}
