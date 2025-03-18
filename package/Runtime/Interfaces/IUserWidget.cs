using UnityEngine;

namespace Eu4ng.Framework.OutGame
{
    public interface IUserWidget
    {
        RectTransform Prefab { get; set; }

        bool IsPrefab => Prefab == null;

        void Show();

        void Hide();

        void Remove();

        bool IsGlobalWidget { get; set; }
    }
}
