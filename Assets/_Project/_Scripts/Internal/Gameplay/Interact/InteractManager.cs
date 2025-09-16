using System;
using Internal.Core.Inputs;
using UnityEngine;
using Zenject;

namespace Internal.Gameplay.Interact
{
    public class InteractManager : MonoBehaviour
    {
        [SerializeField] private Transform _outRay;
        [SerializeField] private float _interactionDistance = 1.5f;

        private InputReader _inputReader;

        [Inject]
        private void Construct(InputReader inputReader)
        {
            _inputReader = inputReader;
        }

        private void OnEnable()
        {
            _inputReader.OnInteractInput += TryInteract;
        }

        private void OnDisable()
        {
            if (_inputReader != null)
            {
                _inputReader.OnInteractInput -= TryInteract;
            }
        }

        private void TryInteract(bool isInteractButtonPressed)
        {
            if (!Physics.Raycast(_outRay.position, _outRay.forward, out var hit, _interactionDistance))
                return;

            var interacts = hit.collider.GetComponents<IInteractable>();
            if (interacts.Length == 0) return;

            if(interacts.Length > 1) 
                Array.Sort(interacts, (a, b) => b.Priority.CompareTo(a.Priority));
            
            foreach (var interact in interacts)
            {
                interact.Interact(isInteractButtonPressed);
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Debug.DrawRay(_outRay.position, _outRay.forward * _interactionDistance, Color.cyan);
        }
#endif
    }
}