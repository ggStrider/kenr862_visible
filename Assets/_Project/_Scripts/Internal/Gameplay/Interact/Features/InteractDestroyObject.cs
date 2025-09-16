using UnityEngine;
using UnityEngine.Events;

namespace Internal.Gameplay.Interact.Features
{
    public class InteractDestroyObject : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject _target;

        [Space] [SerializeField] private UnityEvent _onDestroy;
        
        public int Priority { get; set; } = 0;

        private bool _destroyed;
        
        public void Interact(bool isInteractButtonPressed)
        {
            if (!isInteractButtonPressed) return;
            if (_destroyed) return;
            _destroyed = true;
            
            _onDestroy?.Invoke();
            Destroy(_target);
        }

#if UNITY_EDITOR
        private void Reset()
        {
            if (_target == null)
            {
                _target = gameObject;
            }
        }
#endif
    }
}