using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Eu4ng.Framework.OutGame
{
    /// <summary>
    /// 일반적인 게임들의 메인 메뉴 위젯 클래스입니다.
    /// </summary>
    public class MainMenuBase : UserWidget
    {
        [Header("References")]
        [SerializeField] protected Button m_PlayButton;
        [SerializeField] protected Button m_OptionsButton;
        [SerializeField] protected Button m_ExitButton;

        [Header("Config")]
        [SerializeField] protected bool m_UseSceneIndex = true;
        [SerializeField] protected int m_MainSceneIndex = 1;
        [SerializeField] protected string m_MainSceneName = "Main";
        [SerializeField] protected RectTransform m_OptionsWidget;

        /* MonoBehaviour */

        protected override void Awake()
        {
            base.Awake();

            // Bind button events
            if (m_PlayButton != null) m_PlayButton.onClick.AddListener(OnPlayButtonClicked);
            if (m_OptionsButton != null) m_OptionsButton.onClick.AddListener(OnOptionsButtonClicked);
            if (m_ExitButton != null) m_ExitButton.onClick.AddListener(OnExitButtonClicked);
        }

        /* MainMenuBase */

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

        public virtual void OnExitButtonClicked()
        {
            Debug.Log("Exit Button Clicked");
            Exit();
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

        protected virtual void Exit()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
