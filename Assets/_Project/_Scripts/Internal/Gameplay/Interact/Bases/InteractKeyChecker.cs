using Definitions.GameItems.Scripts;
using Internal.Core.DataModel;
using UnityEngine;
using Zenject;

namespace Internal.Gameplay.Interact.Bases
{
    public abstract class InteractKeyChecker : MonoBehaviour, IInteractable
    {
        [SerializeField] private KeyGameItemSO _keyToUnlock;
        public int Priority { get; set; } = 0;

        private PlayerData _playerData;

        [Inject]
        private void Construct(PlayerData playerData)
        {
            _playerData = playerData;
        }

        public void Interact(bool isInteractButtonPressed)
        {
            if (!isInteractButtonPressed) return;
            if (_playerData.GameItems.Contains(_keyToUnlock))
            {
                OnKeyMatched();
            }
            else
            {
                OnNoMatchedKey();
            }
        }

        protected abstract void OnKeyMatched();
        protected virtual void OnNoMatchedKey() { }
    }
}