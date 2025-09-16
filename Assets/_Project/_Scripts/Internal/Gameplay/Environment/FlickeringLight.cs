using Internal.Core.Tools;
using UnityEngine;
using Utilities;

namespace Gameplay
{
    public class FlickeringLight : MonoTick
    {
        [SerializeField] private Light _flickeringLight;
        
        [SerializeField] private float _flickerSpeed = 7f;
        
        [SerializeField] private float _minIntensity = 0f;
        [SerializeField] private float _maxIntensity = 3;
        
        // [Space] [Header("Can be null")]
        // [SerializeField] private VolumetricLightBeam _lightBeam;
        // [SerializeField, Min(1)] private float _divisionLightBeamIntensity = 10;

        private int _noiseSeed;

        protected override void OnEnable()
        {
            _noiseSeed = Random.Range(0, int.MaxValue);
            base.OnEnable();
        }

        public void FlickerForTime(float time)
        {
            StartTick();
            CooldownRunner.Run(time, StopTick);
        }
        
        protected override void OnTickUpdated()
        {
            var noise = Mathf.PerlinNoise(Time.time * _flickerSpeed, _noiseSeed);
            var intensity = Mathf.Lerp(_minIntensity, _maxIntensity, noise);
            
            _flickeringLight.intensity = intensity;
            
            // if(_lightBeam) _lightBeam.intensityInside = intensity / _divisionLightBeamIntensity;
        }
        
#if UNITY_EDITOR
        private void Reset()
        {
            _flickeringLight ??= GetComponent<Light>();
            _flickeringLight ??= GetComponentInChildren<Light>();
        }
#endif
    }
}