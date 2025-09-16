using UnityEngine;

using System.Linq;
using System.Collections.Generic;

namespace Internal.Gameplay.Hover
{
    public class PlayerHoverManager : MonoBehaviour
    {
        [SerializeField] private Transform _outPoint;
        [SerializeField] private float _distance = 2f;
        
        private RaycastHit _hitInfo;

        private HashSet<IHoverableGameObject> _hoveredObjects = new HashSet<IHoverableGameObject>();
        
        private void Update()
        {
            if (Physics.Raycast(_outPoint.position, _outPoint.forward, out _hitInfo, _distance))
            {
                if (_hitInfo.collider.TryGetComponent(out IHoverableGameObject _))
                {
                    var newHovered = _hitInfo.collider?.GetComponents<IHoverableGameObject>().ToHashSet();
                    if (newHovered != null && !_hoveredObjects.SetEquals(newHovered))
                    {
                        ClearHovered(ref _hoveredObjects);
                        
                        _hoveredObjects = newHovered;
                        InvokeAllHovered(_hoveredObjects);
                    }
                }
                else
                {
                    ClearHovered(ref _hoveredObjects);
                }
            }
            // Raycast == null
            else
            {
                ClearHovered(ref _hoveredObjects);
            }
        }

        private static void ClearHovered(ref HashSet<IHoverableGameObject> hoveredObjects)
        {
            if(hoveredObjects.Count == 0) return;
            foreach (var hovered in hoveredObjects)
            {
                if (hovered != null)
                {
                    hovered.OnUnHover();
                }
            }
            hoveredObjects.Clear();
        }

        private void InvokeAllHovered(HashSet<IHoverableGameObject> hoveredObjects)
        {
            foreach (var hovered in hoveredObjects)
            {
                hovered.OnHover();
            }
        }
        
        #if UNITY_EDITOR
        private void Reset()
        {
            if (!_outPoint)
            {
                _outPoint = Camera.main.transform;
            }
        }
#endif
    }
}