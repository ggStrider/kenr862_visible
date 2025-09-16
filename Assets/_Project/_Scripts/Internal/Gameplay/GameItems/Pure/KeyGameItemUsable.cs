using UnityEngine;

namespace Internal.Gameplay.GameItems.Pure
{
    public class KeyGameItemUsable : IUsableGameItem
    {
        public void UseItem(bool isUseButtonPressed)
        {
            Debug.Log("i'm just-- a... a fish...");
        }

        public void OnSwitchThisItem()
        {
        }
    }
}