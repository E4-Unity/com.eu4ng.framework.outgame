using System;
using System.Collections;
using Eu4ng.Manager.Singleton;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Eu4ng.Framework.OutGame
{
    public class SceneLoadingManager : MonoSingleton<SceneLoadingManager>
    {
        /* Fields */
        [Header("Config")]
        [SerializeField] float m_MinimumLoadingScreenDisplayTime = 1.0f;
        [SerializeField] float m_FadeTime = 1.0f;

        /* Properties */

        IUIManager UIManagerInterface => UIManager.Instance;
        OutGameFrameworkSettings Settings => OutGameFrameworkSettings.Instance;

        /* MonoSingleton */

        protected override void OnInitialize() {}

        /* SceneLoadingManager */

        public void LoadSceneImmediately<T>(T scene) where T : struct, IConvertible
        {
            LogOutGameFramework.Log("LoadSceneImmediately: " + scene);
            SceneManager.LoadScene(Convert.ToInt32(scene));
        }

        public void LoadScene<T>(T scene) where T : struct, IConvertible
        {
            // Check LoadingWidgetPrefab
            if (Settings.LoadingWidgetPrefab == null ||
                Settings.LoadingWidgetPrefab.GetComponent<ILoadingWidget>() == null)
            {
                LoadSceneImmediately(scene);
                return;
            }

            // Show LoadingWidgetPrefab
            UIManagerInterface.ShowWidget(Settings.LoadingWidgetPrefab);
            var loadingWidget = UIManagerInterface.GetWidget(Settings.LoadingWidgetPrefab);
            var loadingWidgetInterface = loadingWidget.GetComponent<ILoadingWidget>();

            // Start Loading
            StartCoroutine(LoadSceneCoroutine(Convert.ToInt32(scene), loadingWidgetInterface));
        }

        protected virtual IEnumerator LoadSceneCoroutine(int buildIndex, ILoadingWidget loadingWidgetInterface)
        {
            loadingWidgetInterface.HideLoadingScreen();
            loadingWidgetInterface.FadeOut(m_FadeTime);
            yield return new WaitForSeconds(m_FadeTime);
            loadingWidgetInterface.UpdateLoadingState("Loading");
            loadingWidgetInterface.ShowLoadingScreen();

            var operation = SceneManager.LoadSceneAsync(buildIndex);
            operation.allowSceneActivation = false;

            while (!operation.isDone)
            {
                loadingWidgetInterface.UpdateLoadingProgress(operation.progress / 0.9f);

                yield return null;
            }

            loadingWidgetInterface.UpdateLoadingState("Complete");
            yield return new WaitForSeconds(m_MinimumLoadingScreenDisplayTime);
            loadingWidgetInterface.HideLoadingScreen();
            loadingWidgetInterface.FadeIn(m_FadeTime);
            yield return new WaitForSeconds(m_FadeTime);
        }
    }
}
