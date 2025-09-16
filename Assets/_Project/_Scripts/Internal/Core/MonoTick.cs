using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Tools;
using UnityEngine;

namespace Utilities
{
    public abstract class MonoTick : MonoBehaviour
    {
        [SerializeField, Min(0.001f)] protected float TickInterval = 0.02f;
        [SerializeField] private bool _startOnEnable = true;
        
        private CancellationTokenSource _tickCts;

        protected abstract void OnTickUpdated();

        protected virtual void OnEnable()
        {
            if (_startOnEnable)
            {
                StartTick();
            }
        }

        protected void OnDisable()
        {
            StopTick();
        }

        protected void StartTick()
        {
            MyUniTaskExtensions.SafeCancelAndCleanToken(ref _tickCts, createNewTokenAfter: true);
            CountTick().Forget();
        }

        protected void StopTick()
        {
            MyUniTaskExtensions.SafeCancelAndCleanToken(ref _tickCts);
        }

        private void OnDestroy()
        {
            StopTick();
        }

        private async UniTaskVoid CountTick()
        {
            try
            {
                while (!_tickCts.IsCancellationRequested)
                {
                    await UniTask.WaitForSeconds(TickInterval, cancellationToken: _tickCts.Token);
                    OnTickUpdated();
                }
            }
            catch (OperationCanceledException)
            {
            }
        }
    }
}