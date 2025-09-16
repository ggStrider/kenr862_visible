using Internal.Gameplay.GameItems;
using UnityEngine;

namespace Definitions.GameItems.Scripts
{
    [CreateAssetMenu(fileName = "Tablet Watcher CCTV", menuName =
        StaticKeys.PROJECT_NAME + "/GameItems/Tablet Watcher CCTV")]
    public class TabletWatcherCCTV : GameItemSO
    {
        public override IUsableGameItem CreateInstance()
        {
            return null;
        }
    }
}