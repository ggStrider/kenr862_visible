using Internal.Core.Audio;
using UnityEngine;

namespace Internal.Gameplay.GameItems.Pure
{
    public class GameItemSoundPlayer : IUsableGameItem
    {
        [SerializeField] private AudioPlayer _audioPlayer;
        
        public void UseItem(bool isUseButtonPressed)
        {
            if (!isUseButtonPressed) return;
            
            _audioPlayer.PlayRandomShot();
        }

        public void OnSwitchThisItem()
        {
        }
    }
}