using UnityEngine;

namespace Eu4ng.Framework.OutGame.Sample
{
    public enum SampleSceneType
    {
        MainMenu,
        Main
    }

    [CreateAssetMenu(fileName = "SampleSceneWidgetsConfig", menuName = "Scriptable Objects/OutGameFramework/SampleSceneWidgetsConfig")]
    public class SampleSceneWidgetsConfig : SceneWidgetsConfig<SampleSceneType>
    {
        
    }
}
