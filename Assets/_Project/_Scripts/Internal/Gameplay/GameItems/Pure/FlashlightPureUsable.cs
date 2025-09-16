using Internal.Core.Audio;
using UnityEngine;

namespace Internal.Gameplay.GameItems.Pure
{
    public class FlashlightPureUsable : IUsableGameItem
    {
        [SerializeField] private Light _light;
        [SerializeField] private float _enabledIntensity = 10f;

        [Space] [SerializeField] private AudioPlayer _audioPlayer;
        
        [SerializeField] private bool _isEnabled;
        
        public void UseItem(bool isUseButtonPressed)
        {
            if (!isUseButtonPressed) return;
            
            _isEnabled = !_isEnabled;
            if (_isEnabled)
            {
                _light.intensity = _enabledIntensity;
            }
            else
            {
                _light.intensity = 0;
            }
            
            _audioPlayer?.PlayRandomShot();
        }

        public void OnSwitchThisItem()
        {
        }
    }
}