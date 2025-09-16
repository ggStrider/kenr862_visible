using System.Threading;
using Cysharp.Threading.Tasks;
using Internal.Core.Audio;
using Tools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Internal.Gameplay.Audio
{
    public class SoundPlayerInRandomTiming : MonoBehaviour
    {
        [SerializeField] private AudioPlayer _audioPlayer;

        [Space] [SerializeField, Min(0.001f)] private float _minDelay = 10;
        [SerializeField, Min(0.002f)] private float _maxDelay = 15;

        [Space] [SerializeField] private bool _startLogicOnEnable = true;

        private CancellationTokenSource _waitAndPlayCts;

        private void OnEnable()
        {
            if (_startLogicOnEnable)
            {
                StartCounting();
            }
        }

        private void OnDisable()
        {
            StopCounting();
        }

        private void StartCounting()
        {
            MyUniTaskExtensions.SafeCancelAndCleanToken(ref _waitAndPlayCts, true);
            WaitAndPlay().Forget();
        }

        private void StopCounting()
        {
            MyUniTaskExtensions.SafeCancelAndCleanToken(ref _waitAndPlayCts);
        }

        private async UniTaskVoid WaitAndPlay()
        {
            while (!_waitAndPlayCts.IsCancellationRequested)
            {
                var waitTime = Random.Range(_minDelay, _maxDelay);
                await UniTask.WaitForSeconds(waitTime, cancellationToken: _waitAndPlayCts.Token);
                
                _audioPlayer.PlayRandomShot();
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_minDelay > _maxDelay)
            {
                _minDelay = _maxDelay - 0.001f;
            }
        }
#endif
    }
}