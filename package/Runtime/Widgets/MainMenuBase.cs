using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Eu4ng.Framework.OutGame
{
    /// <summary>
    /// 일반적인 게임들의 메인 메뉴 위젯 클래스입니다.
    /// </summary>
    public class MainMenuBase : UserWidget
    {
        [SerializeField] protected bool m_UseSceneIndex = true;
        [SerializeField] protected int m_MainSceneIndex = 1;
        [SerializeField] protected string m_MainSceneName = "Main";
        [SerializeField] protected RectTransform m_OptionsWidget;

        public virtual void OnPlayButtonClicked()
        {
            Debug.Log("Play Button Clicked");
            OpenMainScene();
        }

        public virtual void OnOptionsButtonClicked()
        {
            Debug.Log("Options Button Clicked");
            ShowOptionsWidget();
        }

        public virtual void OnQuitButtonClicked()
        {
            Debug.Log("Quit Button Clicked");
            Quit();
        }

        protected virtual void OpenMainScene()
        {
            if (m_UseSceneIndex)
            {
                SceneManager.LoadScene(m_MainSceneIndex);
            }
            else
            {
                SceneManager.LoadScene(m_MainSceneName);
            }
        }

        protected virtual void ShowOptionsWidget()
        {
            AddWidget(m_OptionsWidget);
        }

        protected virtual void Quit()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
