using System;
using System.Collections.Generic;
using Eu4ng.Manager.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Eu4ng.Framework.OutGame
{
    [Serializable]
    public struct UserWidgetBindingData
    {
        public string InputActionName;

        public RectTransform WidgetPrefab;

        public bool IsValid => WidgetPrefab != null;

        public bool IsNotValid => !IsValid;
    }

    [CreateAssetMenu(fileName = "InputConfig_UserWidget", menuName = "Scriptable Objects/InputConfig/UserWidget")]
    public class InputConfig_UserWidget : InputConfig
    {
        [Header("Config")]
        [SerializeField] List<UserWidgetBindingData> m_DataList = new List<UserWidgetBindingData>();

        protected List<UserWidgetBindingData> DataList => m_DataList;

        /* InputConfig */

        public override List<InputBindingData> BindActions(InputActionMap inputActionMap)
        {
            var inputBindingDataList = new List<InputBindingData>();
            if (inputActionMap == null) return inputBindingDataList;

            foreach (var data in DataList)
            {
                // 유효성 검사
                if (data.IsNotValid) continue;

                // 입력 액션 가져오기
                var inputAction = GetInputAction(inputActionMap, data.InputActionName);
                if (inputAction == null) continue;

                // 입력 바인딩
                var inputBindingData = new InputBindingData
                {
                    Action = inputAction,
                    PerformedActions = new List<Action<InputAction.CallbackContext>>()
                };

                Action<InputAction.CallbackContext> toggleWidgetAction = ctx =>
                {
                    UIManager.Instance.ToggleWidget(data.WidgetPrefab);
                };

                inputBindingData.Action.performed += toggleWidgetAction;
                inputBindingData.PerformedActions.Add(toggleWidgetAction);

                // 입력 바인딩 목록에 추가
                inputBindingDataList.Add(inputBindingData);
            }

            return inputBindingDataList;
        }
    }
}
