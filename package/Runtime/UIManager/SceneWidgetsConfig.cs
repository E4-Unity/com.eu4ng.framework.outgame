using System;
using System.Collections.Generic;
using UnityEngine;

namespace Eu4ng.Framework.OutGame
{
    /// <summary>
    /// 씬 별로 생성할 기본 위젯 목록 설정
    /// </summary>
    public abstract class SceneWidgetsConfig : ScriptableObject
    {
        [SerializeField] List<RectTransform> m_StartupWidgetPrefabs;

        public abstract int BuildIndex { get; }

        public List<RectTransform> StartupWidgetPrefabs => m_StartupWidgetPrefabs;
    }

    public abstract class SceneWidgetsConfig<T> : SceneWidgetsConfig where T : struct, IConvertible
    {
        [SerializeField] T m_BuildIndex;

        public override int BuildIndex => Convert.ToInt32(m_BuildIndex);
    }
}
