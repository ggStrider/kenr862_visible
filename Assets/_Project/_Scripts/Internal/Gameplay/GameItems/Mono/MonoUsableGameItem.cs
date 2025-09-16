using Sirenix.OdinInspector;
using UnityEngine;

namespace Internal.Gameplay.GameItems.Mono
{
    public abstract class MonoUsableGameItem : MonoBehaviour, IUsableGameItem
    {
        protected abstract IUsableGameItem UsableGameItem { get; }
        
        #if UNITY_EDITOR
        [Button]
        private void Test_EditorOnly()
        {
            UseItem(true);
        }
        #endif
        
        public void UseItem(bool isUseButtonPressed)
        {
            UsableGameItem?.UseItem(isUseButtonPressed);
        }

        public void OnSwitchThisItem()
        {
            UsableGameItem?.OnSwitchThisItem();
        }
    }
}