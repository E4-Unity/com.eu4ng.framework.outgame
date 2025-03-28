using System;
using UnityEngine;

namespace Eu4ng.Framework.OutGame
{
    /// <summary>
    /// 버튼 클릭 시 설정된 씬으로 이동
    /// </summary>
    public abstract class ChangeSceneButton<T> : ButtonWidget where T : struct, IConvertible
    {
        [SerializeField] T m_TargetScene;

        protected override void OnButtonClicked()
        {
            base.OnButtonClicked();

            SceneLoadingManager.Instance.LoadScene(m_TargetScene);
        }
    }
}
