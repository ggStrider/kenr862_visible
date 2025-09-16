using UnityEngine;
using UnityEngine.Events;

namespace Internal.Gameplay.Environment.CollideEvents
{
    public class TriggerEnter : MonoBehaviour
    {
        [SerializeField] private string _tag;
        
        // oh fuck
        [SerializeField] private UnityEvent _action;

        [SerializeField] private bool _canInvokeOnce;
        private bool _canInvoke = true;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_tag) && _canInvoke)
            {
                _action?.Invoke();

                if (_canInvokeOnce)
                {
                    _canInvoke = false;
                }
            }
        }
    }
}