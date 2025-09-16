using System;
using Definitions.Cassettes;
using Definitions.GameItems.Scripts;
using Internal.Core.Reactive;

namespace Internal.Core.DataModel
{
    [Serializable]
    public class PlayerData
    {
        public ReactiveList<GameItemSO> GameItems = new();
        public ReactiveList<CassetteSO> CollectedCassettes = new();
    }
}