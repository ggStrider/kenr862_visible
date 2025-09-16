using UnityEngine;
using UnityEngine.Events;

namespace Internal.Gameplay.Interact.Features
{
    public class InteractAddItemAndAction : InteractAddItemIntoInventory
    {
        [SerializeField] private UnityEvent _action;
        
        protected override void OnAdded()
        {
            _action?.Invoke();
        }
    }
}