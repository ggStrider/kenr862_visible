using Internal.Gameplay.GameItems.Pure;
using UnityEngine;

namespace Internal.Gameplay.GameItems.Mono
{
    public class MaterialChangerMonoUsable : MonoUsableGameItem
    {
        [SerializeReference] private GameItemMaterialChanger _gameItemMaterialChanger = new();
        protected override IUsableGameItem UsableGameItem => _gameItemMaterialChanger;
    }
}