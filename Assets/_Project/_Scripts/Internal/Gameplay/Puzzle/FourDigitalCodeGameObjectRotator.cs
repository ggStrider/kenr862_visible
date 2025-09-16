using DG.Tweening;
using Internal.Core.Audio;
using Internal.Core.Tools;
using UnityEngine;

namespace Internal.Gameplay.Puzzle
{
    public class FourDigitalCodeGameObjectRotator : FourDigitCodePanel
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _openedPosition;

        [Space] [SerializeField] private ParticleSystem _particle;
        [SerializeField] private AudioPlayer _onOpen;

        [SerializeField] private float _delayBeforeOpen = 2f;
        [SerializeField] private float _openDuration = 1.8f;
        
        protected override void OnSuccessfulEntered()
        {
            _onOpen.PlayRandomShot();
            _particle.Play();
            CooldownRunner.Run(_delayBeforeOpen, () => _target.DOLocalRotate(_openedPosition, _openDuration));
        }
    }
}