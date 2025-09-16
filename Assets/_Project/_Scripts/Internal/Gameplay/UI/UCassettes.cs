using Definitions.Cassettes;
using Internal.Core.DataModel;
using Internal.Core.Inputs;
using Internal.Core.UI;
using Internal.Core.UI.Bases;
using Internal.Gameplay.GameItems.Pure;
using Internal.Gameplay.Interact.Features;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Internal.Gameplay.UI
{
    public class UCassettes : MonoBehaviour, IEscClosable
    {
        [SerializeField] private GridLayoutGroup _grid;
        [SerializeField] private UCassetteContainer _uiPrefab;

        [Space] [SerializeField] private GameObject _onNoCassettes;
        [SerializeField, ReadOnly] private int _cassettesOnScene;

        [Space] [SerializeField] private TextMeshProUGUI _cassettesCountLabel;

        // im sorry for this, I hadn't enough time
        public static UCassettes Instance;

        private InputReader _inputReader;
        private PlayerData _playerData;
        private EscManager _escManager;

        private CassettePlayerGameItemUsable _cassettePlayer;
        
        [Inject]
        private void Construct(PlayerData playerData, EscManager escManager, InputReader inputReader)
        {
            _playerData = playerData;
            _escManager = escManager;
            _inputReader = inputReader;

            Instance = this;
        }

        private void Awake()
        {
            _cassettesOnScene = FindObjectsByType<InteractAddCassetteIntoInventory>
                (FindObjectsInactive.Exclude, FindObjectsSortMode.None).Length;
        }

        public void SetWhoGetsCallback(CassettePlayerGameItemUsable getsCallback)
        {
            _cassettePlayer = getsCallback;
        }

        public void GetCallback(CassetteSO cassette)
        {
            _cassettePlayer?.PlayVideo(cassette);
            _escManager.HandleEsc();
        }

        private void OnEnable()
        {
            DestroyAllChildren();
            SpawnAllCassettes();

            _inputReader.EnableMapUI();
            Cursor.lockState = CursorLockMode.None;
            
            _escManager.AddToStack(this);
            _cassettesCountLabel.text = $"{_playerData.CollectedCassettes.Count}/{_cassettesOnScene}";
        }

        private void SpawnAllCassettes()
        {
            _onNoCassettes?.SetActive(_playerData.CollectedCassettes.Count == 0);

            for (var i = 0; i < _playerData.CollectedCassettes.Count; i++)
            {
                var cassette = _playerData.CollectedCassettes[i];
                var spawned = Instantiate(_uiPrefab, _grid.transform);
                
                spawned.CassetteSO = cassette;
                spawned.Label.text = cassette.CassetteName;
                spawned.Button.onClick.AddListener(() => GetCallback(spawned.CassetteSO));
            }

            _cassettesCountLabel.text = $"{_playerData.CollectedCassettes}/{_cassettesOnScene}";
        }
        
        private void DestroyAllChildren()
        {
            for (var i = 0; i < _grid.transform.childCount; i++)
            {
                var child = _grid.transform.GetChild(i);
                Destroy(child.gameObject);
            }
        }

        public void Close()
        {
            _inputReader.EnableMapPlayer();
            Cursor.lockState = CursorLockMode.Locked;
            gameObject.SetActive(false);
        }
    }
}
