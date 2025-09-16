using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Internal.Core.Inputs
{
    public class InputReader : IInitializable, IDisposable
    {
        private PlayerInputMap _playerInputMap;

        public bool IsPlayerMapEnabled()
        {
            return _playerInputMap.Player.enabled;
        }

        public void Initialize()
        {
            _playerInputMap = new();

            SubscribeIntoPlayerMapEvents();
            SubscribeIntoUIMapEvents();
            InitializeAlwaysEnabledMap();

            _playerInputMap.Player.Enable();
        }

        public void Dispose()
        {
            _playerInputMap.Player.Disable();
            _playerInputMap.UI.Disable();

            UnsubscribeFromPlayerMapEvents();
            UnsubscribeFromUIMapEvents();
            ClearPlayerEvents();
            ClearUIEvents();
            ClearAlwaysEnabledMap();

            _playerInputMap.Dispose();
        }

        public void EnableMapPlayer()
        {
            _playerInputMap.UI.Disable();
            _playerInputMap.Player.Enable();
        }

        public void EnableMapUI()
        {
            _playerInputMap.UI.Enable();
            _playerInputMap.Player.Disable();
        }

        public void DisableAllMaps()
        {
            _playerInputMap.UI.Disable();
            _playerInputMap.Player.Disable();
            _playerInputMap.AlwaysEnabled.Disable();
        }

        public void DisablePlayerMap()
        {
            _playerInputMap.Player.Disable();
        }

        public void EnableAlwaysEnabledMap()
        {
            _playerInputMap.AlwaysEnabled.Enable();
        }

        #region Always Enabled Map

        public event Action OnEscPressedInput;
            
        private void InitializeAlwaysEnabledMap()
        {
            _playerInputMap.AlwaysEnabled.Esc.performed += EscPressedNotify;
            _playerInputMap.AlwaysEnabled.Enable();
        }
        
        private void ClearAlwaysEnabledMap()
        {
            _playerInputMap.AlwaysEnabled.Esc.performed -= EscPressedNotify;
            OnEscPressedInput = null;
            _playerInputMap.AlwaysEnabled.Disable();
        }
        
        private void EscPressedNotify(InputAction.CallbackContext context)
        {
            OnEscPressedInput?.Invoke();
        }

        #endregion

        #region Player Maps

        public event Action<Vector2> OnMoveInput;
        public event Action<Vector2> OnLookInput;
        public event Action<bool> OnJumpInput;
        public event Action<bool> OnCrouchInput;
        public event Action<bool> OnInteractInput;
        public event Action<bool> OnUseItemInHandsInput;
        public event Action<int> OnScrollGameItemsInput;
        public event Action<bool> OnSprintInput;

        private void SubscribeIntoPlayerMapEvents()
        {
            _playerInputMap.Player.Move.performed += MoveNotify;
            _playerInputMap.Player.Move.canceled += MoveNotify;

            _playerInputMap.Player.Look.performed += LookNotify;
            _playerInputMap.Player.Look.canceled += LookNotify;

            _playerInputMap.Player.Sprint.performed += SprintNotify;
            _playerInputMap.Player.Sprint.canceled += SprintNotify;

            _playerInputMap.Player.Jump.performed += JumpNotify;
            _playerInputMap.Player.Jump.canceled += JumpNotify;

            _playerInputMap.Player.Crouch.performed += CrouchNotify;
            _playerInputMap.Player.Crouch.canceled += CrouchNotify;

            _playerInputMap.Player.Interact.performed += InteractNotify;
            _playerInputMap.Player.Interact.canceled += InteractNotify;

            _playerInputMap.Player.UseGameItem.performed += UseGameItemNotify;
            _playerInputMap.Player.UseGameItem.canceled += UseGameItemNotify;

            _playerInputMap.Player.ScrollGameItems.performed += ScrollGameItemsNotify;
        }

        private void UnsubscribeFromPlayerMapEvents()
        {
            _playerInputMap.Player.Move.performed -= MoveNotify;
            _playerInputMap.Player.Move.canceled -= MoveNotify;

            _playerInputMap.Player.Look.performed -= LookNotify;
            _playerInputMap.Player.Look.canceled -= LookNotify;
            
            _playerInputMap.Player.Sprint.performed -= SprintNotify;
            _playerInputMap.Player.Sprint.canceled -= SprintNotify;

            _playerInputMap.Player.Jump.performed -= JumpNotify;
            _playerInputMap.Player.Jump.canceled -= JumpNotify;

            _playerInputMap.Player.Crouch.performed -= CrouchNotify;
            _playerInputMap.Player.Crouch.canceled -= CrouchNotify;

            _playerInputMap.Player.Interact.performed -= InteractNotify;
            _playerInputMap.Player.Interact.canceled -= InteractNotify;

            _playerInputMap.Player.UseGameItem.performed -= UseGameItemNotify;
            _playerInputMap.Player.UseGameItem.canceled -= UseGameItemNotify;
        }
        
        private void ClearPlayerEvents()
        {
            OnMoveInput = null;
            OnLookInput = null;
            OnSprintInput = null;
            OnJumpInput = null;
            OnCrouchInput = null;
            OnInteractInput = null;
            OnUseItemInHandsInput = null;
        }

        private void MoveNotify(InputAction.CallbackContext context)
        {
            var moveDirection = context.ReadValue<Vector2>();
            OnMoveInput?.Invoke(moveDirection);
        }

        private void LookNotify(InputAction.CallbackContext context)
        {
            var look = context.ReadValue<Vector2>();
            OnLookInput?.Invoke(look);
        }
        
        private void SprintNotify(InputAction.CallbackContext context)
        {
            OnSprintInput?.Invoke(context.performed);
        }

        private void JumpNotify(InputAction.CallbackContext context)
        {
            OnJumpInput?.Invoke(context.performed);
        }

        private void CrouchNotify(InputAction.CallbackContext context)
        {
            OnCrouchInput?.Invoke(context.performed);
        }

        private void InteractNotify(InputAction.CallbackContext context)
        {
            OnInteractInput?.Invoke(context.performed);
        }

        private void UseGameItemNotify(InputAction.CallbackContext context)
        {
            OnUseItemInHandsInput?.Invoke(context.performed);
        }

        private void ScrollGameItemsNotify(InputAction.CallbackContext context)
        {
            var side = (int)Mathf.Sign(context.ReadValue<float>());
            OnScrollGameItemsInput?.Invoke(side);
        }

        #endregion

        #region UI Maps

        public event Action<Vector2> OnCodeSwitch;

        private void SubscribeIntoUIMapEvents()
        {
            _playerInputMap.UI.CodeSwitch.performed += CodeSwitchNotify;
        }

        private void UnsubscribeFromUIMapEvents()
        {
            _playerInputMap.UI.CodeSwitch.performed -= CodeSwitchNotify;
        }

        private void CodeSwitchNotify(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<Vector2>();
            OnCodeSwitch?.Invoke(direction);
        }

        private void ClearUIEvents()
        {
            OnCodeSwitch = null;
        }

        #endregion
    }
}