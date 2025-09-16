using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Internal.Gameplay.Interact.Features
{
    public class InteractObjectMover : MonoBehaviour, IInteractable
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _tweenDuration = 0.25f;

        [Space] [Header("Position Values are in LOCAL")] [SerializeField]
        private Vector3 _openedPosition;

        [SerializeField] private Vector3 _closedPosition;

        [Space] [SerializeField] private bool _isClosed = true;

        private bool _isPlayingAnimation;

        public int Priority { get; set; } = 0;

        public void Interact(bool isInteractButtonPressed)
        {
            if (!isInteractButtonPressed) return;
            if (_isPlayingAnimation) return;
            _isPlayingAnimation = true;
            _isClosed = !_isClosed;

            var nextPosition = _isClosed ? _closedPosition : _openedPosition;
            _target.DOLocalMove(nextPosition, _tweenDuration).OnComplete(() => _isPlayingAnimation = false);
        }

#if UNITY_EDITOR
        private void Reset()
        {
            if (_target == null)
            {
                _target = transform;
            }

            if (!_target.TryGetComponent<Collider>(out _))
            {
                Debug.LogError("No Collider on this object!");
            }
        }

        [Button]
        private void SetPositionForClosed()
        {
            _closedPosition = _target.localPosition;
            _isClosed = true;
        }

        [Button]
        private void SetPositionForOpen()
        {
            _openedPosition = _target.localPosition;
            _isClosed = false;
        }

        [Button]
        private void Close()
        {
            _target.localPosition = _closedPosition;
            _isClosed = true;
        }

        [Button]
        private void Open()
        {
            _target.localPosition = _openedPosition;
            _isClosed = false;
        }
#endif
    }
}