using UnityEngine;

namespace Internal.Gameplay.GameItems
{
    public class SmoothFollower : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _positionSpeed = 10f;
        [SerializeField] private float _rotationSpeed = 10f;

        private void LateUpdate()
        {
            transform.position = Vector3.Lerp(
                transform.position,
                _target.position,
                Time.deltaTime * _positionSpeed
            );

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                _target.rotation,
                Time.deltaTime * _rotationSpeed
            );
        }
    }
}
