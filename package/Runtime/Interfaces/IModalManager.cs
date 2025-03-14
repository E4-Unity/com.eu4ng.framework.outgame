using System;
using UnityEngine;

namespace Eu4ng.Framework.OutGame
{
    public interface IModalManager
    {
        void RequestAlert(in ModalRequestData requestData, RectTransform widgetPrefab = null);

        void RequestConfirm(in ModalRequestData requestData, RectTransform widgetPrefab = null);

        void RequestPrompt(in ModalRequestData requestData, RectTransform widgetPrefab = null);
    }
}
