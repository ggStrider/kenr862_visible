using System;
using Internal.Core.Extensions;
using Internal.Core.Tools;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Internal.Core.Audio
{
    [Serializable]
    public class AudioPlayer
    {
        public AudioSource audioSource;
        public AudioClip[] clips;

        [Range(0f, 1f)] public float volume = 0.4f;

        [Space]
        public bool useRandomPitch = true;
        
        [Space]
        [Range(-3f, 3f), ShowIf(nameof(useRandomPitch))] public float minPitch = 0.95f;
        [Range(-3f, 3f), ShowIf(nameof(useRandomPitch))] public float maxPitch = 1.05f;

        public void PlayShot(int index)
        {
            if (index < 0)
            {
                LogHandler.LogWarning(this, "Index cannot be < 0");
                return;
            }

            if (index >= clips.Length)
            {
                LogHandler.LogWarning(this, "Index greater than clips length");
                return;
            }
            
            PlayShotOfClip(clips[index]);
        }

        public void PlayRandomShot()
        {
            if (!clips.NotNullAndHasElements())
            {
                LogHandler.LogError(this, $"{nameof(clips)} array is null or doesn't have any element ");
                return;
            }
            
            PlayShotOfClip(clips.GetRandomElement());
        }

        public void PlayShotOfClip(AudioClip clip)
        {
            audioSource.volume = volume;
            if (useRandomPitch) audioSource.pitch = Random.Range(minPitch, maxPitch);
            
            audioSource.PlayOneShot(clip);
        }
    }
}