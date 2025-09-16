using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Internal.Gameplay.Interact
{
    [Serializable]
    public abstract class SpamInteractButton
    {
        [field: SerializeField] public GameObject UI { get; private set; }
        [SerializeField] private Slider _progress;

        [Space] [SerializeField] private float _waitTimeToDecrease = 0.1f;
        [SerializeField] private float _decreaseDelta = 0.1f;

        [Space] [SerializeField] private float _increaseValue = 0.05f;

        public bool Completed => _progress.value >= _progress.maxValue;

        private CancellationTokenSource _decreaseCts;

        public virtual void PressedButton()
        {
            if (Completed) return;
            _progress.value = Mathf.Min(_progress.value + _increaseValue, _progress.maxValue);
            
            if (Completed)
            {
                ToggleDecreasing(active: false);
                OnComplete();
            }
        }

        protected abstract void OnComplete();
        protected virtual void OnStartedSpamming() { }

        public void ToggleDecreasing(bool active)
        {
            if (active)
            {
                _progress.gameObject.SetActive(true);
                MyUniTaskExtensions.SafeCancelAndCleanToken(ref _decreaseCts, true);
                OnStartedSpamming();
                DecreaseByTime().Forget();
            }
            else
            {
                MyUniTaskExtensions.SafeCancelAndCleanToken(ref _decreaseCts, false);
            }
        }

        private async UniTaskVoid DecreaseByTime()
        {
            try
            {
                while (!_decreaseCts.IsCancellationRequested)
                {
                    _progress.value = Mathf.Clamp(_progress.value - _decreaseDelta,
                        _progress.minValue, _progress.maxValue);
                        
                    await UniTask.WaitForSeconds(_waitTimeToDecrease, cancellationToken: _decreaseCts.Token);
                }
            }
            catch (OperationCanceledException)
            {
            }
        }
    }
}