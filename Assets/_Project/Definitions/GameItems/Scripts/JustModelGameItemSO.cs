using Internal.Gameplay.GameItems;
using Internal.Gameplay.GameItems.Pure;
using UnityEngine;

namespace Definitions.GameItems.Scripts
{
    [CreateAssetMenu(fileName = "New Just Model", menuName = 
        StaticKeys.PROJECT_NAME + "/GameItems/Just Model")]
    public class JustModelGameItemSO : GameItemSO
    {
        public override IUsableGameItem CreateInstance()
        {
            return new JustGameItemUsable();
        }
    }
}
