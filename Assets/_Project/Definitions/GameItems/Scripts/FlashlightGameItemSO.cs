using Internal.Gameplay.GameItems;
using UnityEngine;

namespace Definitions.GameItems.Scripts
{
    [CreateAssetMenu(fileName = "Flashlight", menuName = 
        StaticKeys.PROJECT_NAME + "/GameItems/Flashlight")]
    public class FlashlightGameItemSO : GameItemSO
    {
        public override IUsableGameItem CreateInstance()
        {
            return null;
        }
    }
}