using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Eu4ng.Framework.OutGame
{
    /// <summary>
    /// OutGameFramework 패키지 전용 함수 라이브러리
    /// </summary>
    public static class OutGameFrameworkFunctionLibrary
    {
        public static void Exit()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public static void ShowOptionsWidget()
        {
            var settings = OtherSettings.Instance;
            if(settings.OptionsWidget != null) UIManager.Instance.ShowWidget(settings.OptionsWidget);
        }
    }
}
