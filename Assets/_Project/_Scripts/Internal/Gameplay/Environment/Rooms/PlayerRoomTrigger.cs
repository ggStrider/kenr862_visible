using UnityEngine;
using Zenject;

namespace Internal.Gameplay.Environment.Rooms
{
    public class PlayerRoomTrigger : MonoBehaviour
    {
        [field: SerializeField] public Room Room { get; private set; }

        [Space] [SerializeField] private bool _setThisRoomAsStartRoom;
        
        private PlayerRoomManager _playerRoomManager;
        
        [Inject]
        private void Construct(PlayerRoomManager playerRoomManager)
        {
            _playerRoomManager = playerRoomManager;
        }

        private void Start()
        {
            if(_setThisRoomAsStartRoom) _playerRoomManager.SetRoomWherePlayerNow(Room);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(StaticKeys.PLAYER_TAG))
            {
                _playerRoomManager.SetRoomWherePlayerNow(Room);
            }
        }
    }
}