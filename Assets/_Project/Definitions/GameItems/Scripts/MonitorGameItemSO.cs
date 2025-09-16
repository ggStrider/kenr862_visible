using Internal.Gameplay.GameItems;
using UnityEngine;

namespace Definitions.GameItems.Scripts
{
    [CreateAssetMenu(fileName = "Monitor Game Item", menuName = 
        StaticKeys.PROJECT_NAME + "/GameItems/Monitor")]
    public class MonitorGameItemSO : GameItemSO
    {
        public override IUsableGameItem CreateInstance()
        {
            return null;
        }
    }
}