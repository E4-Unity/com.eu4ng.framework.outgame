using System.Collections.Generic;
using Eu4ng.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Eu4ng.Framework.OutGame
{
    public class SceneWidgetsManager : MonoBehaviour
    {
        [Header("State")]
        [SerializeField, ReadOnly] List<RectTransform> m_GlobalWidgets = new List<RectTransform>();
        [SerializeField, ReadOnly] List<RectTransform> m_SceneWidgets = new List<RectTransform>();

        OutGameFrameworkSettings Settings => OutGameFrameworkSettings.Instance;
        IUIManager UIManagerInterface => UIManager.Instance;

        protected virtual void Awake()
        {
            CreateGlobalWidgets();

            SceneManager.activeSceneChanged += OnActiveSceneChanged;
        }

        protected virtual void CreateGlobalWidgets()
        {
            if (m_GlobalWidgets.Count > 0) return;

            foreach (var globalWidgetPrefab in Settings.GlobalWidgetPrefabs)
            {
                UIManagerInterface.ShowWidget(globalWidgetPrefab);
                var globalWidget = UIManagerInterface.GetWidgetInstance(globalWidgetPrefab);
                m_GlobalWidgets.Add(globalWidget);
            }
        }

        protected virtual void OnActiveSceneChanged(Scene currentScene, Scene nextScene)
        {
            DestroySceneWidgets();

            CreateSceneWidgets(nextScene.buildIndex);
        }

        protected virtual void DestroySceneWidgets()
        {
            foreach (var sceneWidget in m_SceneWidgets)
            {
                IUserWidget userWidgetInterface = sceneWidget.GetComponent<IUserWidget>();
                userWidgetInterface?.Remove();
            }

            m_SceneWidgets.Clear();
        }

        protected virtual void CreateSceneWidgets(int buildIndex)
        {
            var sceneWidgetPrefabs = Settings.GetStartupWidgets(buildIndex);
            foreach (var sceneWidgetPrefab in sceneWidgetPrefabs)
            {
                UIManagerInterface.ShowWidget(sceneWidgetPrefab);
                var globalWidget = UIManagerInterface.GetWidgetInstance(sceneWidgetPrefab);
                m_SceneWidgets.Add(globalWidget);
            }
        }
    }
}
