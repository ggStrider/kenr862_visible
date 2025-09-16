using Sirenix.OdinInspector;
using UnityEngine;

namespace Internal.Gameplay.GameItems.Pure
{
    public class GameItemMaterialChanger : IUsableGameItem
    {
        [SerializeField] private MeshRenderer _renderer;
        [SerializeField] private Material _defaultMaterial;
        [SerializeField] private Material _changedMaterial;

        [SerializeField, ReadOnly] private bool _changed;
        
        public void UseItem(bool isUseButtonPressed)
        {
            if (!isUseButtonPressed) return;
            
            _changed = !_changed;
            _renderer.material = _changed ? _changedMaterial : _defaultMaterial;
        }

        public void OnSwitchThisItem()
        {
        }
    }
}