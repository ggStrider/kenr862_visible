using Definitions.Cassettes;
using Internal.Gameplay.UI;
using UnityEngine;
using UnityEngine.Video;

namespace Internal.Gameplay.GameItems.Pure
{
    public class CassettePlayerGameItemUsable : IUsableGameItem
    {
        // [SerializeField] private AudioSource _cassetteAudioSource;
        
        [SerializeField] private VideoPlayer _cassetteOutput;
        
        public void UseItem(bool isUseButtonPressed)
        {
            if (!isUseButtonPressed) return;
            if (_cassetteOutput.isPlaying)
            {
                _cassetteOutput.Stop();
            }
            else
            {
                UCassettes.Instance.gameObject.SetActive(true);
                UCassettes.Instance.SetWhoGetsCallback(this);
            }
        }

        public void OnSwitchThisItem()
        {
        }

        public void PlayVideo(CassetteSO cassette)
        {
            _cassetteOutput.clip = cassette.VideoClip;
            _cassetteOutput.Play();
        }
    }
}