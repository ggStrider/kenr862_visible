using Internal.Core.Audio;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Internal.Gameplay.UI
{
    public class USoundPlayerOnHover : MonoBehaviour, IPointerEnterHandler
    {
        [SerializeField] private AudioPlayer _audioPlayer;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            _audioPlayer.PlayRandomShot();
        }
    }
}