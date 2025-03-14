using UnityEngine;
using UnityEngine.UI;

namespace Eu4ng.Framework.OutGame
{
    // TODO Alert, Confirm, Prompt로 분리
    public class ModalWidgetBase : UserWidget, IModalWidget
    {
        [SerializeField] Button m_ConfirmButton;
        [SerializeField] Button m_CancelButton;
        [SerializeField] InputField m_InputField;

        /* IModalWidget */

        public ModalRequestData RequestData { get; set; }

        /* ModalWidgetBase */

        protected virtual void OnConfirmButtonClicked()
        {
            Debug.Log("ConfirmButton Clicked.");
            if(m_InputField != null) RequestData.InputSubmitted?.Invoke(m_InputField.text);
            RequestData.Confirmed?.Invoke();

            Hide();
        }

        protected virtual void OnCancelButtonClicked()
        {
            Debug.Log("CancelButton Clicked.");
            RequestData.Canceled?.Invoke();

            Hide();
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
