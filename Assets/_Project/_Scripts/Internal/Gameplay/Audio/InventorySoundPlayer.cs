using Core.DataModel;
using Definitions.Cassettes;
using Definitions.GameItems.Scripts;
using Internal.Core.Audio;
using Internal.Core.DataModel;
using UnityEngine;
using Zenject;

namespace Internal.Gameplay.Audio
{
    public class InventorySoundPlayer : MonoBehaviour
    {
        [SerializeField] private AudioPlayer _pickupPlayer;
        [SerializeField] private AudioPlayer _selectedItemChanged;
        
        private PlayerData _playerData;
        private Inventory _inventory;
        
        [Inject]
        private void Construct(PlayerData playerData, Inventory inventory)
        {
            _playerData = playerData;
            _inventory = inventory;
        }

        private void OnEnable()
        {
            _playerData.GameItems.OnListCountChanged += PlayGameItemPickupSound;
            _playerData.CollectedCassettes.OnListCountChanged += PlayCassettePickupSound;

            _inventory.OnSelectedItemChanged += PlayChangedItemSound;
        }
        
        private void OnDisable()
        {
            if (_playerData != null)
            {
                _playerData.GameItems.OnListCountChanged -= PlayGameItemPickupSound;
                _playerData.CollectedCassettes.OnListCountChanged -= PlayCassettePickupSound;
            }

            if (_inventory != null)
            {
                _inventory.OnSelectedItemChanged -= PlayChangedItemSound;
            }
        }

        private void PlayCassettePickupSound(int index, CassetteSO cassette)
        {
            _pickupPlayer.PlayRandomShot();
        }

        private void PlayGameItemPickupSound(int index, GameItemSO gameItem)
        {
            _pickupPlayer.PlayRandomShot();
        }
        
        private void PlayChangedItemSound(Inventory.ItemWithInstance arg1, Inventory.ItemWithInstance arg2)
        {
            _selectedItemChanged.PlayRandomShot();
        }

        public void _PlayPickupSound()
        {
            _pickupPlayer.PlayRandomShot();
        }
    }
}