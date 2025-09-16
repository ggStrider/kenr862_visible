using DG.Tweening;
using Internal.Core.UI;
using Internal.Core.UI.Bases;
using KinematicCharacterController.Examples;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Zenject;

namespace Internal.Gameplay.UI
{
    public class UTutorialTextThrower : MonoBehaviour, IEscClosable
    {
        [SerializeField] private CanvasGroup _tutorialGroup;
        [SerializeField] private TextMeshProUGUI _tutorialLabel;

        [Space] [SerializeField] private float _tweenDuration = 0.5f;
        [SerializeField] private Volume _volume;

        private DepthOfField _depthOfField;

        private ExamplePlayer _player;
        private EscManager _escManager;
        private Tween _currentTween;

        [Inject]
        private void Construct(EscManager escManager, ExamplePlayer player)
        {
            _escManager = escManager;
            _player = player;
        }

        private void Awake()
        {
            _volume.profile.TryGet(out _depthOfField);
            _depthOfField.focalLength.value = 1;
        }

        public void ThrowTutorial(string tutorialText)
        {
            _player.enabled = false;
            Time.timeScale = 0;
            _escManager.AddToStack(this);
            _tutorialLabel.text = tutorialText;
            ChangeAlphaOfGroup(1);

            DOVirtual.Float(_depthOfField.focalLength.value, 27, _tweenDuration,
                    x => _depthOfField.focalLength.value = x)
                .SetUpdate(true); 
        }

        private void ChangeAlphaOfGroup(float to)
        {
            _currentTween?.Kill();
            _currentTween = DOVirtual.Float(_tutorialGroup.alpha, to, _tweenDuration,
                x => _tutorialGroup.alpha = x).SetUpdate(true);
        }

        public void Close()
        {
            _player.enabled = true;
            Time.timeScale = 1;
            ChangeAlphaOfGroup(0);
            DOVirtual.Float(_depthOfField.focalLength.value, 1, _tweenDuration, x => _depthOfField.focalLength.value = x);
        }
    }
}