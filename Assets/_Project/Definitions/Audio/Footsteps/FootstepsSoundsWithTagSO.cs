using System;
using UnityEngine;

namespace Definitions.Audio.Footsteps
{
    [CreateAssetMenu(fileName = "New Footsteps with Tag", menuName = StaticKeys.PROJECT_NAME +
                                                                     "/Audio/Footsteps with Tag")]
    public class FootstepsSoundsWithTagSO : ScriptableObject
    {
        [field: SerializeField] public SoundsAndTag[] FootstepsSoundsAndTags { get; private set; }

        public AudioClip[] GetClipsByTag(string tag)
        {
            foreach (var container in FootstepsSoundsAndTags)
            {
                if (container.Tag == tag) return container.Clips;
            }

            return Array.Empty<AudioClip>();
        }

        [Serializable]
        public class SoundsAndTag
        {
            [field: SerializeField] public string Tag { get; private set; }
            [field: SerializeField] public AudioClip[] Clips { get; private set; }
        }
    }
}