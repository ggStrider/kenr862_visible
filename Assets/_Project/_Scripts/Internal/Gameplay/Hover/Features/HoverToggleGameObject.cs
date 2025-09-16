using System;
using UnityEngine;

namespace Internal.Gameplay.Hover.Features
{
    public class HoverToggleGameObject : MonoBehaviour, IHoverableGameObject
    {
        [field: SerializeField] public GameObject Target { get; private set; }
        private bool _canUnhover = true;
        
        public void OnHover()
        {
            Target.SetActive(true);
        }

        public void OnUnHover()
        {
            if(_canUnhover) Target.SetActive(false);
        }

        private void OnDestroy()
        {
            _canUnhover = false;
            Target.SetActive(true);
        }
    }
}