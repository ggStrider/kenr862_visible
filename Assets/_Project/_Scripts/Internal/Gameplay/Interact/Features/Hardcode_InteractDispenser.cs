using Definitions.GameItems.Scripts;
using Internal.Core.Audio;
using Internal.Core.DataModel;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Internal.Gameplay.Interact.Features
{
    public class Hardcode_InteractDispenser : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameItemSO _requiredItem;

        [Space] [SerializeField] private AudioPlayer _audioPlayer;
        [SerializeField] private GameObject _toEnable;
        [SerializeField] private GameObject _toDisable;

        [Space] [SerializeField] private UnityEvent _action;
        
        private bool _used;
        
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
            if (_used) return;
            
            if (_playerData.GameItems.Contains(_requiredItem))
            {
                _used = true;
                _action?.Invoke();
                
                _toEnable.SetActive(true);
                _toDisable.SetActive(false);
                _audioPlayer.PlayRandomShot();
            }
        }

        public void _SetUsed() => _used = true;
    }
}