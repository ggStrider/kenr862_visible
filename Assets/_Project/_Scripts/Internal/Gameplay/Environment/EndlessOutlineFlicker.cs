using DG.Tweening;
using UnityEngine;

namespace Internal.Gameplay.Environment
{
    public class EndlessOutlineFlicker : MonoBehaviour
    {
        [SerializeField] private Outline _outline;
        [SerializeField, Range(0f, 10f)] private float _minOutlineWidth = 0.8f;
        [SerializeField, Range(0f, 10f)] private float _maxOutlineWidth = 4.5f;
        [SerializeField, Min(0.01f)] private float _tweenDuration = 0.7f;

        private Tween _tween;

        private void OnEnable()
        {
            FlickerOutline();
        }

        private void OnDisable()
        {
            _tween?.Kill();
        }

        private void FlickerOutline()
        {
            _tween?.Kill();
            
            _tween = DOVirtual.Float(_minOutlineWidth, _maxOutlineWidth, _tweenDuration,
                x => _outline.OutlineWidth = x)
                .SetLoops(-1, LoopType.Yoyo);
        }

#if UNITY_EDITOR
        private void Reset()
        {
            if (_outline == null)
            {
                if (TryGetComponent<Outline>(out var outline))
                {
                    _outline = outline;
                }
                else
                {
                    _outline = gameObject.AddComponent<Outline>();
                }
            }
        }

        private void OnValidate()
        {
            if(enabled) FlickerOutline();
        }
#endif
    }
}