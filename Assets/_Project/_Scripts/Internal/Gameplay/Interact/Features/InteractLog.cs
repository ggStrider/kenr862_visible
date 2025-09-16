using UnityEngine;

namespace Internal.Gameplay.Interact.Features
{
    public class InteractLog : MonoBehaviour, IInteractable
    {
        public int Priority { get; set; } = 0;
        
        public void Interact(bool isInteractButtonPressed)
        {
            var state = isInteractButtonPressed ? "down" : "up";
            Debug.Log($"[{GetType().Name}] State: {state}");
        }
    }
}