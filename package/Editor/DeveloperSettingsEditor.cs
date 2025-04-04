using UnityEditor;
using Eu4ng.Manager.Singleton;

namespace Eu4ng.Framework.OutGame.Editor
{
    internal static class DeveloperSettingsEditor
    {
        [MenuItem("Tools/DeveloperSettings/Generate/UIManagerSettings")]
        static void GenerateUIManagerSettings() => DeveloperSettings.CreateDeveloperSettings(typeof(UIManagerSettings));
    }
}
