using Internal.Gameplay.GameItems;
using UnityEngine;

namespace Definitions.GameItems.Scripts
{
    [CreateAssetMenu(fileName = "Stapler Game Item", menuName = 
        StaticKeys.PROJECT_NAME + "/GameItems/Stapler")]
    public class StaplerGameItemSO : GameItemSO
    {
        public override IUsableGameItem CreateInstance()
        {
            return null;
        }
    }
}