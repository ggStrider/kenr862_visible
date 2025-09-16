using Sirenix.OdinInspector;
using UnityEngine;

namespace Internal.Gameplay.GameItems.Pure
{
    public class MaterialChangerUsablePure : IUsableGameItem
    {
        [SerializeField] private Material[] _materials;
        [SerializeField] private Renderer _output;
        
        [Space, ReadOnly]
        [SerializeField] private int _currentMaterial;
        
        public void UseItem(bool isUseButtonPressed)
        {
            if(!isUseButtonPressed) return;
            
            _currentMaterial = (int)Mathf.Repeat(_currentMaterial + 1, _materials.Length);
            _output.material = _materials[_currentMaterial];
        }

        public void OnSwitchThisItem()
        {
        }
    }
}