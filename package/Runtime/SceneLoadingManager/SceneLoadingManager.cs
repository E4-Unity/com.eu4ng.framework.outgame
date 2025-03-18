using System;
using System.Collections;
using Eu4ng.Manager.Singleton;
using Eu4ng.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Eu4ng.Framework.OutGame
{
    public enum LoadingStateType
    {
        None,
        FadeOut,
        Loading,
        Complete,
        FadeIn,
        Done
    }

    public class SceneLoadingManager : MonoSingleton<SceneLoadingManager>
    {
        /* Fields */
        [Header("Config")]
        [SerializeField] float m_MinimumLoadingScreenDisplayTime = 2.0f;
        [SerializeField] float m_FadeTime = 2.0f;

        [Header("State")]
        [SerializeField, ReadOnly] LoadingStateType m_LoadingState = LoadingStateType.None;

        public event Action<LoadingStateType> LoadingStateChanged;

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
            SetState(LoadingStateType.FadeOut);
            loadingWidgetInterface.HideLoadingScreen();
            loadingWidgetInterface.FadeOut(m_FadeTime);
            yield return new WaitForSeconds(m_FadeTime);

            // 로딩창 표시
            SetState(LoadingStateType.Loading);
            loadingWidgetInterface.UpdateLoadingProgress(0);
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
            SetState(LoadingStateType.Complete);
            loadingWidgetInterface.UpdateLoadingProgress(1);
            loadingWidgetInterface.UpdateLoadingState("Complete");

            // 고정 로딩 시간동안 대기
            yield return new WaitForSeconds(m_MinimumLoadingScreenDisplayTime);

            // 씬 전환
            loadingWidgetInterface.HideLoadingScreen();
            operation.allowSceneActivation = true;
            while (!operation.isDone)
            {
                yield return null;
            }

            // 페이드 인
            SetState(LoadingStateType.FadeIn);
            loadingWidgetInterface.FadeIn(m_FadeTime);
            yield return new WaitForSeconds(m_FadeTime);

            // 종료
            SetState(LoadingStateType.Done);
        }

        protected virtual void SetState(LoadingStateType loadingState)
        {
            if (m_LoadingState == loadingState) return;
            m_LoadingState = loadingState;

            LoadingStateChanged?.Invoke(m_LoadingState);

            LogOutGameFramework.Log("LoadingState Changed: " + m_LoadingState);
        }
    }
}
