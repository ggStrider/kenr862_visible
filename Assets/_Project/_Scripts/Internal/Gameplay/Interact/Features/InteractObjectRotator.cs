using System;
using DG.Tweening;
using Internal.Core.Audio;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Internal.Gameplay.Interact.Features
{
    public class InteractObjectRotator : MonoBehaviour, IInteractable
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _tweenDuration = 0.25f;

        [Space] [Header("Rotation Values are in LOCAL")]
        [SerializeField] private Vector3 _openedRotation;
        [SerializeField] private Vector3 _closedRotation;

        [Space] [SerializeField] private bool _shouldDisableColliderWhileRotating = true;
        [ShowIf(nameof(_shouldDisableColliderWhileRotating))] private Collider _collider;
        
        [Space] 
        [SerializeField] private bool _hasSounds = false;
        
        [ShowIf(nameof(_hasSounds))]
        [SerializeField] private AudioPlayer _onOpen;
        
        [ShowIf(nameof(_hasSounds))]
        [SerializeField] private AudioPlayer _onClose;
        
        [Space] [SerializeField] private bool _isClosed = true;

        private bool _isPlayingAnimation;

        public int Priority { get; set; } = 0;

        private void Awake()
        {
            _collider ??= GetComponent<Collider>();
        }

        public void Interact(bool isInteractButtonPressed)
        {
            if (!isInteractButtonPressed) return;
            if (_isPlayingAnimation) return;
            _isPlayingAnimation = true;
            _isClosed = !_isClosed;

            var nextRotation = _isClosed ? _closedRotation : _openedRotation;
            if(_hasSounds) (_isClosed ? _onClose : _onOpen).PlayRandomShot();

            if (_shouldDisableColliderWhileRotating)
            {
                _collider.enabled = false;
            }
            _target.DOLocalRotate(nextRotation, _tweenDuration).OnComplete(() =>
            {
                _isPlayingAnimation = false;

                if (_shouldDisableColliderWhileRotating)
                {
                    _collider.enabled = true;
                }
            });
        }

#if UNITY_EDITOR
        private void Reset()
        {
            if (_target == null)
            {
                _target = transform;
            }

            if (!_target.TryGetComponent<Collider>(out var col))
            {
                Debug.LogError("No Collider on this object!");
            }
        }

        [Button]
        private void SetPositionForClosed()
        {
            _closedRotation = _target.localRotation.eulerAngles;
            _isClosed = true;
        }

        [Button]
        private void SetPositionForOpen()
        {
            _openedRotation = _target.localRotation.eulerAngles;
            _isClosed = false;
        }

        [Button]
        private void Close()
        {
            _target.localRotation = Quaternion.Euler(_closedRotation);
            _isClosed = true;
        }

        [Button]
        private void Open()
        {
            _target.localRotation = Quaternion.Euler(_openedRotation);
            _isClosed = false;
        }
#endif
    }
}