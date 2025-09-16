using Internal.Gameplay.GameItems.Pure;
using UnityEngine;

namespace Internal.Gameplay.GameItems.Mono
{
    public class CameraWatcherCCTVMonoUsable : MonoUsableGameItem
    {
        [SerializeReference] private MaterialChangerUsablePure _materialChangerPure = new();
        protected override IUsableGameItem UsableGameItem => _materialChangerPure;
    }
}