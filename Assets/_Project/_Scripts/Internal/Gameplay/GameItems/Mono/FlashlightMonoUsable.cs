using Internal.Gameplay.GameItems.Pure;
using UnityEngine;

namespace Internal.Gameplay.GameItems.Mono
{
    public class FlashlightMonoUsable : MonoUsableGameItem
    {
        [SerializeReference] private FlashlightPureUsable _flashlight = new();
        protected override IUsableGameItem UsableGameItem => _flashlight;
    }
}