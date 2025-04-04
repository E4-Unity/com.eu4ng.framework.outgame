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

    public class SceneLoadingManager : GameSubsystem<SceneLoadingManager>
    {
        /* Fields */

        [field: Header("State")]
        [field: SerializeField, ReadOnly] public LoadingStateType LoadingState { get; private set; } = LoadingStateType.None;

        public event Action<LoadingStateType> LoadingStateChanged;

        /* Properties */

        IUIManager UIManagerInterface => UIManager.Instance;
        SceneLoadingManagerSettings Settings => SceneLoadingManagerSettings.Instance;

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
            var settings = Settings;

            // Check LoadingWidgetPrefab
            if (settings.LoadingWidgetPrefab == null ||
                settings.LoadingWidgetPrefab.GetComponent<ILoadingWidget>() == null)
            {
                LoadSceneImmediately(scene);
                return;
            }

            // Show LoadingWidgetPrefab
            UIManagerInterface.ShowWidget(settings.LoadingWidgetPrefab);
            var loadingWidget = UIManagerInterface.GetWidget(settings.LoadingWidgetPrefab);
            var loadingWidgetInterface = loadingWidget.GetComponent<ILoadingWidget>();

            // Start Loading
            StartCoroutine(LoadSceneCoroutine(Convert.ToInt32(scene), loadingWidgetInterface));
        }

        protected virtual IEnumerator LoadSceneCoroutine(int buildIndex, ILoadingWidget loadingWidgetInterface)
        {
            var settings = Settings;

            // 페이드 아웃
            SetState(LoadingStateType.FadeOut);
            loadingWidgetInterface.HideLoadingScreen();
            loadingWidgetInterface.FadeOut(settings.FadeTime);
            yield return new WaitForSeconds(settings.FadeTime);

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
            yield return new WaitForSeconds(settings.MinimumLoadingScreenDisplayTime);

            // 씬 전환
            loadingWidgetInterface.HideLoadingScreen();
            operation.allowSceneActivation = true;
            while (!operation.isDone)
            {
                yield return null;
            }

            // 페이드 인
            SetState(LoadingStateType.FadeIn);
            loadingWidgetInterface.FadeIn(settings.FadeTime);
            yield return new WaitForSeconds(settings.FadeTime);

            // 종료
            SetState(LoadingStateType.Done);
        }

        protected virtual void SetState(LoadingStateType loadingState)
        {
            if (LoadingState == loadingState) return;
            LoadingState = loadingState;

            LoadingStateChanged?.Invoke(LoadingState);

            LogOutGameFramework.Log("LoadingState Changed: " + LoadingState);
        }
    }
}
