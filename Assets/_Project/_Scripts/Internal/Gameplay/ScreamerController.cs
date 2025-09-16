using Internal.Core;
using Internal.Gameplay.UI;
using UnityEngine;
using UnityEngine.Playables;
using Zenject;
using KinematicCharacterController.Examples;

namespace Internal.Gameplay.Wtf
{
    // ai generated
    public class ScreamerController : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private PlayableDirector _playableDirector;

        [SerializeField] private Transform _screamerAnchor;
        [SerializeField] private GameObject _spawnedFakeEnemy;
        [SerializeField] private Transform _playerCamera;

        [Space] [SerializeField] private UFader _fader;
        [SerializeField] private PlayableAsset _screamer;

        private Transform _originalCameraParent;
        private Vector3 _originalCameraLocalPos;
        private Quaternion _originalCameraLocalRot;

        private SceneLoader _sceneLoader;
        private ExamplePlayer _examplePlayer;

        [Inject]
        private void Construct(SceneLoader sceneLoader, ExamplePlayer player)
        {
            _sceneLoader = sceneLoader;
            _examplePlayer = player;
        }

        public void PlayScreamer(Transform realEnemy)
        {
            realEnemy.gameObject.SetActive(false);
            _examplePlayer.enabled = false;

            // --- Переміщаємо anchor ---
            _screamerAnchor.position = realEnemy.position;
            _screamerAnchor.rotation = realEnemy.rotation;

            // --- Ворога спавним ---
            _spawnedFakeEnemy.SetActive(true);
            _spawnedFakeEnemy.transform.localPosition = Vector3.zero;
            _spawnedFakeEnemy.transform.localRotation = Quaternion.identity;
            _spawnedFakeEnemy.transform.SetParent(_screamerAnchor, worldPositionStays: false);

            // --- Камеру прикріплюємо до anchor ---
            _originalCameraParent = _playerCamera.parent;
            _originalCameraLocalPos = _playerCamera.localPosition;
            _originalCameraLocalRot = _playerCamera.localRotation;

            _playerCamera.SetParent(_screamerAnchor, worldPositionStays: false);

            // --- Запускаємо Timeline ---
            _playableDirector.Play(_screamer);
            _playableDirector.stopped += OnEnd;
        }

        private void OnEnd(PlayableDirector obj)
        {
            _playableDirector.stopped -= OnEnd;
            _fader.FadeInstant();
            _sceneLoader.ReloadScene();
        }

        public void StopScreamer()
        {
            // --- Ворога ховаємо ---
            if (_spawnedFakeEnemy != null)
                _spawnedFakeEnemy.SetActive(false);

            // --- Повертаємо камеру назад ---
            if (_playerCamera != null && _originalCameraParent != null)
            {
                _playerCamera.SetParent(_originalCameraParent, worldPositionStays: false);
                _playerCamera.localPosition = _originalCameraLocalPos;
                _playerCamera.localRotation = _originalCameraLocalRot;
            }
        }
    }
}