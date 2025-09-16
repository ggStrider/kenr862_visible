using UnityEngine;
using UnityEngine.Events;

namespace Internal.Gameplay.Environment
{
    public class IsChildObjectsZero : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onZero;

        private bool _wasZero;
        
        public void _Check()
        {
            if (_wasZero) return;
            
            if (transform.childCount == 1)
            {
                _wasZero = true;
                _onZero?.Invoke();
            }
        }
    }
}