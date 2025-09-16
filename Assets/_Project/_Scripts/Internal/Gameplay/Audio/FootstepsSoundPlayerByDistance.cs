using Definitions.Audio.Footsteps;
using Internal.Core.Audio;
using Internal.Core.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities;

namespace Internal.Gameplay.Audio
{
    public class FootstepsSoundPlayerByDistance : MonoTick
    {
        [SerializeField, Required] private Transform _walkingTarget;
        [SerializeField, Min(0.001f)] private float _distanceToWalk = 1.2f;

        [Space] [SerializeField] private AudioPlayer _audioPlayer;
        [Space] [SerializeField, ReadOnly] private Vector3 _lastPositionSoundPlayed;

        [Space] [SerializeField] private FootstepsSoundsWithTagSO _footstepsSoundsWithTag;

        private void Awake()
        {
            _lastPositionSoundPlayed = _walkingTarget.position;
        }

        protected override void OnTickUpdated()
        {
            if ((_walkingTarget.position - _lastPositionSoundPlayed).sqrMagnitude >= _distanceToWalk * _distanceToWalk)
            {
                _lastPositionSoundPlayed = _walkingTarget.position;

                if (Physics.Raycast(_walkingTarget.position, -_walkingTarget.up,
                        out var hit, 5, ~0, QueryTriggerInteraction.Collide))
                {
                    var clips = _footstepsSoundsWithTag.GetClipsByTag(hit.collider.gameObject.tag);
                    if (clips.Length > 0)
                    {
                        _audioPlayer.PlayShotOfClip(clips.GetRandomElement());
                    }
                }
            }
        }

#if UNITY_EDITOR
        private void Reset()
        {
            if (_walkingTarget == null)
            {
                _walkingTarget = transform;
            }
        }
#endif
    }
}