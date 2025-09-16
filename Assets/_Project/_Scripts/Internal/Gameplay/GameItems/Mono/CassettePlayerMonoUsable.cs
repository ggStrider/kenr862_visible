using Internal.Gameplay.GameItems.Pure;
using UnityEngine;

namespace Internal.Gameplay.GameItems.Mono
{
    public class CassettePlayerMonoUsable : MonoUsableGameItem
    {
        [SerializeReference] private CassettePlayerGameItemUsable _cassettePlayer = new();
        protected override IUsableGameItem UsableGameItem => _cassettePlayer;
    }
}