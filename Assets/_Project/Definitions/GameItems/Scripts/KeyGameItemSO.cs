using Internal.Gameplay.GameItems;
using Internal.Gameplay.GameItems.Pure;
using UnityEngine;

namespace Definitions.GameItems.Scripts
{
    [CreateAssetMenu(fileName = "New Game Key", menuName = 
        StaticKeys.PROJECT_NAME + "/GameItems/Game Key")]
    public class KeyGameItemSO : GameItemSO
    {
        public override IUsableGameItem CreateInstance()
        {
            return new KeyGameItemUsable();
        }
    }
}