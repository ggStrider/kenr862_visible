using Definitions.GameItems.Scripts;
using Internal.Core.DataModel;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Internal.Gameplay.Interact.Features
{
    public class InteractAddItemIntoInventory : MonoBehaviour, IInteractable
    {
        [SerializeField, InlineEditor] private GameItemSO _itemToAdd;
        
        [Space]
        [SerializeField] private bool _addIntoInventoryIfAlreadyHas = false;
        [SerializeField] private bool _destroyWhenAdded = true;

        [ShowIf(nameof(_destroyWhenAdded))]
        [SerializeField] private GameObject _toDestroy;

        private PlayerData _playerData;
        
        public int Priority { get; set; } = 0;

        [Inject]
        private void Construct(PlayerData playerData)
        {
            _playerData = playerData;
        }

        private void Awake()
        {
            if (_toDestroy == null)
            {
                _toDestroy = gameObject;
            }
        }
        
        public void Interact(bool isInteractButtonPressed)
        {
            if (!isInteractButtonPressed) return;
            if (!_addIntoInventoryIfAlreadyHas)
            {
                if(_playerData.GameItems.Contains(_itemToAdd)) return;
            }
            
            _playerData.GameItems.Add(_itemToAdd);
            if (_destroyWhenAdded)
            {
                Destroy(_toDestroy);
            }

            OnAdded();
        }

        protected virtual void OnAdded()
        {
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_toDestroy == null)
            {
                _toDestroy = gameObject;
            }
        }
        #endif
    }
}