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
            // 페이드 아웃
            loadingWidgetInterface.HideLoadingScreen();
            loadingWidgetInterface.FadeOut(m_FadeTime);
            yield return new WaitForSeconds(m_FadeTime);

            // 로딩창 표시
            loadingWidgetInterface.UpdateLoadingState("Loading");
            loadingWidgetInterface.ShowLoadingScreen();

            // 로딩 시작
            var operation = SceneManager.LoadSceneAsync(buildIndex);
            operation.allowSceneActivation = false;

            while (!Mathf.Approximately(operation.progress, 0.9f))
            {
                loadingWidgetInterface.UpdateLoadingProgress(operation.progress / 0.9f);

                yield return null;
            }

            // 로딩 완료
            loadingWidgetInterface.UpdateLoadingState("Complete");

            // 고정 로딩 시간동안 대기
            yield return new WaitForSeconds(m_MinimumLoadingScreenDisplayTime);

            // 씬 전환
            loadingWidgetInterface.HideLoadingScreen();
            operation.allowSceneActivation = true;

            // 페이드 인
            loadingWidgetInterface.FadeIn(m_FadeTime);
            yield return new WaitForSeconds(m_FadeTime);
        }
    }
}
