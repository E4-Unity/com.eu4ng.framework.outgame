using System;
using UnityEngine;

namespace Eu4ng.Framework.OutGame
{
    public struct ModalRequestData
    {
        public string Title;

        public string Message;

        public Action Confirmed;

        public Action Canceled;

        public Action<string> InputSubmitted;
    }

    public interface IModalWidget
    {
        ModalRequestData RequestData { get; set; }
    }
}
