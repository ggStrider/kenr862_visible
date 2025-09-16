using UnityEngine;

namespace Internal.Core
{
    public class SetCursorStateOnAwake : MonoBehaviour
    {
        [SerializeField] private CursorLockMode _lockMode;
        [SerializeField] private bool _visible;

        private void Awake()
        {
            Cursor.lockState = _lockMode;
            Cursor.visible = _visible;
        }
    }
}