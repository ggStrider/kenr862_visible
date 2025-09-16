using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Internal.Gameplay.Audio
{
    public class PlayMusicDontDestroyOnLoad : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;
        [SerializeField] private AudioClip _music;

        [Space] [SerializeField] private float _fadeIn = 3.5f;
        [SerializeField, Range(0f, 1f)] private float _endVolume = 0.75f;
        
        private void Awake()
        {
            DontDestroyOnLoad(this);
            SceneManager.sceneLoaded += OnSceneChanged;
        }
        
        public void Play()
        {
            _source.clip = _music;
            _source.volume = 0;
            _source.Play();

            DOVirtual.Float(_source.volume, _endVolume, _fadeIn, 
                x => _source.volume = x);
        }
        
        private void OnSceneChanged(Scene scene, LoadSceneMode arg1)
        {
            // menu
            if(scene.buildIndex == 0) Destroy(gameObject);
        }
    }
}