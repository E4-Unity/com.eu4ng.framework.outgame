using UnityEngine;

namespace Eu4ng.Framework.OutGame
{
    public class ExitButton : ButtonWidget
    {
        /* ButtonWidget */

        protected override void OnButtonClicked()
        {
            base.OnButtonClicked();

            ModalRequestData exitRequestData = new ModalRequestData
            {
                Title = "Exit Game",
                Message = "Are you sure you want to exit?",
                Confirmed = OutGameFrameworkFunctionLibrary.Exit
            };
            RequestConfirm(exitRequestData);
        }
    }
}
