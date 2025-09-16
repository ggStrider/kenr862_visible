using System;
using System.Threading;
using Cutscenes;
using Cysharp.Threading.Tasks;
using Internal.Core;
using Internal.Core.Audio;
using Internal.Core.Tools;
using Internal.Gameplay.UI;
using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Internal.Gameplay.Interact
{
    // actually bad class, so much logic as fuck
    [Serializable]
    public sealed class SpamInteractChangeScene : SpamInteractButton
    {
        [Space(10)] [SerializeField] private Image _stateImage;

        [Space] [SerializeField] private float _delayToNextFrame = 0.3f;
        [SerializeField] private Sprite _onStartedSpammingAnimationFrame1;
        [SerializeField] private Sprite _onStartedSpammingAnimationFrame2;

        [Space] [SerializeField] private Sprite _onCompleteFrame;
        [SerializeField] private AudioPlayer _onComplete;
        [SerializeField] private AudioPlayer _onSpam;

        private CancellationTokenSource _imageAnimationCts;
        private CutsceneManager _cutsceneManager;
        
        public void Initialize(CutsceneManager cutsceneManager)
        {
            _cutsceneManager = cutsceneManager;
        }
        
        protected override void OnComplete()
        {
            _onComplete.PlayRandomShot();
            MyUniTaskExtensions.SafeCancelAndCleanToken(ref _imageAnimationCts, false);
            _stateImage.sprite = _onCompleteFrame;
            
            _cutsceneManager.PlayEndScene();
        }

        protected override void OnStartedSpamming()
        {
            MyUniTaskExtensions.SafeCancelAndCleanToken(ref _imageAnimationCts, true);
            AnimatePicture().Forget();
        }

        public override void PressedButton()
        {
            base.PressedButton();
            _onSpam.PlayRandomShot();
        }

        private async UniTaskVoid AnimatePicture()
        {
            try
            {
                while (!_imageAnimationCts.IsCancellationRequested)
                {
                    _stateImage.sprite = _onStartedSpammingAnimationFrame1;
                    await UniTask.WaitForSeconds(_delayToNextFrame, cancellationToken: _imageAnimationCts.Token);
                    
                    _stateImage.sprite = _onStartedSpammingAnimationFrame2;
                    await UniTask.WaitForSeconds(_delayToNextFrame, cancellationToken: _imageAnimationCts.Token);
                }
            }
            catch (OperationCanceledException)
            {
            }
        }
    }
}