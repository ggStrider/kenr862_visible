using Internal.Gameplay.UI;
using UnityEngine;
using Zenject;

namespace Internal.Core
{
    public class SceneChangerWithFadeMono : MonoBehaviour
    {
        [SerializeField] private string _sceneToLoad = "EndScene";
        [SerializeField] private float _fadeTime = 2.5f;
        
        private SceneLoader _sceneLoader;
        private UFader _fader;
        
        [Inject]
        private void Construct(UFader fader, SceneLoader sceneLoader)
        {
            _fader = fader;
            _sceneLoader = sceneLoader;
        }

        public void Change()
        {
            if (_fadeTime <= 0)
            {
                _fader.FadeInstant();
                _sceneLoader.LoadScene(_sceneToLoad);
            }
            else
            {
                _fader.Fade(_fadeTime, () => _sceneLoader.LoadScene(_sceneToLoad));
            }
        }
    }
}