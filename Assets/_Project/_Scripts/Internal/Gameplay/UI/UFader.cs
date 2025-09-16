using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Internal.Gameplay.UI
{
    public class UFader : MonoBehaviour
    {
        [SerializeField] private Image _blackscreen;
        [SerializeField] private float _defaultTimeToFade = 0.5f;
        [SerializeField] private float _defaultTimeToUnFade = 0.8f;

        [Space] [SerializeField] private bool _unFadeOnStart = true;

        private void Start()
        {
            if (_unFadeOnStart)
            {
                _blackscreen.color = Color.black;
                UnFade();
            }
        }

        public void UnFade()
        {
            _blackscreen?.DOKill();
            _blackscreen.DOColor(Color.clear, _defaultTimeToUnFade);
        }

        public void Fade()
        {
            _blackscreen?.DOKill();
            _blackscreen.DOColor(Color.black, _defaultTimeToFade);
        }
        
        public void Fade(float time, Action onComplete)
        {
            _blackscreen?.DOKill();
            _blackscreen.DOColor(Color.black, time).OnComplete(onComplete.Invoke);
        }

        public void FadeInstant()
        {
            _blackscreen.color = Color.black;
        }
    }
}
