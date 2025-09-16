using Definitions.Cassettes;
using Internal.Core.DataModel;
using UnityEngine;
using Zenject;

namespace Internal.Gameplay.Interact.Features
{
    public class InteractAddCassetteIntoInventory : MonoBehaviour, IInteractable
    {
        [SerializeField] private CassetteSO _cassette;

        [Space] [SerializeField] private GameObject _toDestroyAfterAdded;
        
        private PlayerData _playerData;
        
        public int Priority { get; set; } = 0;

        [Inject]
        private void Construct(PlayerData playerData)
        {
            _playerData = playerData;
        }
        
        public void Interact(bool isInteractButtonPressed)
        {
            if (!isInteractButtonPressed) return;
            _playerData.CollectedCassettes.Add(_cassette);
            
            Destroy(_toDestroyAfterAdded);
        }
        
        #if UNITY_EDITOR
        private void Reset()
        {
            if (_toDestroyAfterAdded == null)
            {
                _toDestroyAfterAdded = gameObject;
            }
        }
        #endif
    }
}