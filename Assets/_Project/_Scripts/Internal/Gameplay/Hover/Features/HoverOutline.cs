using System;
using Internal.Core.Tools;
using UnityEngine;

namespace Internal.Gameplay.Hover.Features
{
    public class HoverOutline : MonoBehaviour, IHoverableGameObject
    {
        [SerializeField] private Outline _outline;

        [Space] [SerializeField] private bool _disableOutlineOnAwake = true;

        private bool _isDestroyed;

        private void Awake()
        {
            if(_disableOutlineOnAwake) _outline.enabled = false;
        }
        
        public void OnHover()
        {
            if (_isDestroyed) return;
            _outline.enabled = true;
        }

        public void OnUnHover()
        {
            if (_isDestroyed) return;
            _outline.enabled = false;
        }

        private void OnDestroy()
        {
            _isDestroyed = true;
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
#endif
    }
}