using System;
using UnityEngine;

namespace Internal.Gameplay.Environment
{
    public class EndlessObjectRotator : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _direction = new(0, 1, 0);
        [SerializeField] private float _rotateSpeed = 100f;

        private void Update()
        {
            var nextRotation = _target.localRotation.eulerAngles;
            nextRotation += _direction * (_rotateSpeed * Time.deltaTime);

            _target.localRotation = Quaternion.Euler(nextRotation);
        }

#if UNITY_EDITOR
        private void Reset()
        {
            if (_target == null)
            {
                _target = transform;
            }
        }

        private void OnValidate()
        {
            _direction.x = Mathf.Clamp(_direction.x, -1, 1);
            _direction.y = Mathf.Clamp(_direction.y, -1, 1);
            _direction.z = Mathf.Clamp(_direction.z, -1, 1);
        }
#endif
    }
}