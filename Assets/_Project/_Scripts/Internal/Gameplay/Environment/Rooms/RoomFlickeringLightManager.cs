using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Tools;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Internal.Gameplay.Environment.Rooms
{
    public class RoomFlickeringLightManager : MonoBehaviour
    {
        [SerializeField] private Vector2 _flickeringTimeRandom;
        [SerializeField] private Vector2 _timeToFlicker;

        [Space] [SerializeField, Range(0f, 1f)]
        private float _chanceToFlickerWherePlayerNow = 0.25f;

        private PlayerRoomManager _playerRoomManager;
        private CancellationTokenSource _cts;

        [Inject]
        private void Construct(PlayerRoomManager playerRoomManager)
        {
            _playerRoomManager = playerRoomManager;
        }

        private void OnEnable()
        {
            MyUniTaskExtensions.SafeCancelAndCleanToken(ref _cts, createNewTokenAfter: true);
            StartFlickeringAsync(_cts.Token).Forget();
        }

        private void OnDestroy()
        {
            MyUniTaskExtensions.SafeCancelAndCleanToken(ref _cts);
        }

        private async UniTaskVoid StartFlickeringAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var waitTime = Random.Range(_timeToFlicker.x, _timeToFlicker.y);
                await UniTask.Delay(TimeSpan.FromSeconds(waitTime), cancellationToken: token);

                var roomToFlicker = PickRoom();

                if (roomToFlicker?.FlickeringLight != null)
                {
                    var flickerDuration = Random.Range(_flickeringTimeRandom.x, _flickeringTimeRandom.y);
                    roomToFlicker.FlickeringLight.FlickerForTime(flickerDuration);
                }
            }
        }

        private Room PickRoom()
        {
            if (_playerRoomManager.AllRooms.Count == 0) return null;

            if (Random.value <= _chanceToFlickerWherePlayerNow)
            {
                return _playerRoomManager.RoomWherePlayerNow;
            }
            
            return _playerRoomManager.AllRooms[Random.Range(0, _playerRoomManager.AllRooms.Count)];
        }
    }
}
