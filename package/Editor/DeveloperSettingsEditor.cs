using UnityEditor;
using Eu4ng.Manager.Singleton;

namespace Eu4ng.Framework.OutGame.Editor
{
    internal static class DeveloperSettingsEditor
    {
        [MenuItem("Tools/DeveloperSettings/Generate/OutGameFrameworkSettings")]
        static void GenerateOutGameFrameworkSettings() => DeveloperSettings.CreateDeveloperSettings(typeof(OutGameFrameworkSettings));
    }
}
