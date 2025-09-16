using UnityEngine;

namespace Internal.Gameplay.Hover.Features
{
    public class HoverLog : MonoBehaviour, IHoverableGameObject
    {
        public void OnHover()
        {
            Debug.Log("hovered");
        }

        public void OnUnHover()
        {
            Debug.Log("unhovered");
        }
    }
}