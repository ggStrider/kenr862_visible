using Internal.Gameplay.GameItems;
using UnityEngine;

namespace Definitions.GameItems.Scripts
{
    [CreateAssetMenu(fileName = "Cassette Player", menuName = 
        StaticKeys.PROJECT_NAME + "/GameItems/Cassette Player")]
    public class CassettePlayerGameItemSO : GameItemSO
    {
        public override IUsableGameItem CreateInstance()
        {
            return null;
        }
    }
}