using System;
using Internal.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Internal.Gameplay.UI.Menu
{
    public class UButtonSceneChanger : MonoBehaviour
    {
        [SerializeField] private string _sceneName;
        [SerializeField] private Button _toBind;

        [Space] [SerializeField] private Image _blackScreen;

        private void Awake()
        {
            _toBind.onClick.AddListener(OnPressed);
        }

        private void OnPressed()
        {
            _blackScreen.gameObject.SetActive(true);
            _blackScreen.enabled = true;
            _blackScreen.color = Color.black;

            var sceneLoader = new SceneLoader();
            sceneLoader.LoadScene(_sceneName);
        }
    }
}