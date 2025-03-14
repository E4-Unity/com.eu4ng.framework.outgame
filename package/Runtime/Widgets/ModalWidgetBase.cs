using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Eu4ng.Framework.OutGame
{
    // TODO Alert, Confirm, Prompt로 분리
    public class ModalWidgetBase : UserWidget, IModalWidget
    {
        [Header("References")]
        [SerializeField] TextMeshProUGUI m_TitleText;
        [SerializeField] TextMeshProUGUI m_MessageText;
        [SerializeField] Button m_ConfirmButton;
        [SerializeField] Button m_CancelButton;
        [SerializeField] InputField m_InputField;

        private ModalRequestData m_RequestData;

        /* IModalWidget */

        public ModalRequestData RequestData
        {
            get => m_RequestData;
            set
            {
                m_RequestData = value;
                Refresh();
            }
        }

        /* ModalWidgetBase */

        protected virtual void OnConfirmButtonClicked()
        {
            LogOutGameFramework.Log("ConfirmButton Clicked.");
            if(m_InputField != null) RequestData.InputSubmitted?.Invoke(m_InputField.text);
            RequestData.Confirmed?.Invoke();

            Hide();
        }

        protected virtual void OnCancelButtonClicked()
        {
            LogOutGameFramework.Log("CancelButton Clicked.");
            RequestData.Canceled?.Invoke();

            Hide();
        }

        /* UserWidget */

        protected override IUIManager UIManagerInterface => GlobalUIManager.Instance;

        protected override void Refresh()
        {
            base.Refresh();

            if(m_TitleText != null) m_TitleText.text = RequestData.Title;
            if(m_MessageText != null) m_MessageText.text = RequestData.Message;
        }

        /* MonoBehaviour */

        protected override void Awake()
        {
            base.Awake();

            if(m_ConfirmButton != null) m_ConfirmButton.onClick.AddListener(OnConfirmButtonClicked);
            if(m_CancelButton != null) m_CancelButton.onClick.AddListener(OnCancelButtonClicked);
        }
    }
}
