using UnityEngine;

namespace Eu4ng.Framework.OutGame
{
    public interface IUserWidget
    {
        RectTransform Prefab { get; set; }

        void Show();

        void Hide();
    }
}
