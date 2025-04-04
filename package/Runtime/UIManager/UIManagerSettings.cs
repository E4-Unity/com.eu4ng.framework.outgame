using Eu4ng.Manager.Singleton;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Eu4ng.Framework.OutGame
{
    public class UIManagerSettings : DeveloperSettings<UIManagerSettings>
    {
        [field: SerializeField] public Canvas CanvasPrefab { get; private set; }
        [field: SerializeField] public EventSystem EventSystemPrefab { get; private set; }

        protected override void OnInitialize() {}
    }
}
