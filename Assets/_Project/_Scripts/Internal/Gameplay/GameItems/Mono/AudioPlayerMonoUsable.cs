using Internal.Gameplay.GameItems.Pure;
using UnityEngine;

namespace Internal.Gameplay.GameItems.Mono
{
    public class AudioPlayerMonoUsable : MonoUsableGameItem
    {
        [SerializeReference] private GameItemSoundPlayer _audioPlayer = new();
        protected override IUsableGameItem UsableGameItem => _audioPlayer;
    }
}