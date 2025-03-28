using UnityEngine;
using UnityEngine.UI;

namespace Eu4ng.Framework.OutGame
{
    public abstract class ButtonWidget : UserWidget
    {
        /* Fields */

        [SerializeField] Button m_Button;

        /* Properties */

        protected Button GetButton() => m_Button;

        /* ButtonWidget */

        protected virtual void OnButtonClicked()
        {
            LogOutGameFramework.Log(GetButton().name + " clicked.");
        }

        /* UserWidget */

        protected override void AssignReferences()
        {
            base.AssignReferences();

            if (m_Button == null) m_Button = GetComponent<Button>();
        }

        protected override void BindEvents()
        {
            base.BindEvents();

            if (GetButton()) GetButton().onClick.AddListener(OnButtonClicked);
        }
    }
}
