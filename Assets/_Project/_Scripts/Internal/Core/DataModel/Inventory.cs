using System;
using System.Collections.Generic;
using Cutscenes;
using Cysharp.Threading.Tasks;
using Definitions.GameItems.Scripts;
using Internal.Core.DataModel;
using Internal.Core.Inputs;
using Internal.Core.Tools;
using Internal.Gameplay.GameItems;
using Internal.Gameplay.GameItems.Mono;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Core.DataModel
{
    // literally god class. Hell nawh, hate this
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private Transform _spawnedGameItemsParent;
        
        [SerializeField] private PlayerData _playerData;
        [Space] [SerializeField] private List<ItemWithInstance> _itemWithInstances = new();

        [Space] [SerializeField] private List<GameItemSO> _startItems = new();
        [SerializeField] private bool _initializeStartItemsByCutscene = true;
        private int _currentSelectedItemWithInstance = 0;

        private InputReader _inputReader;

        [Space] [SerializeField] private float _delayBetweenScrollingToNextItem = 0.2f;
        private bool _canChangeSelectedItem = true;

        private DiContainer _container;

        /// <summary>
        /// T1 - Old Item, T2 - New item
        /// </summary>
        public event Action<ItemWithInstance, ItemWithInstance> OnSelectedItemChanged; 

        [Inject]
        private void Construct(InputReader inputReader, PlayerData playerData,
            CutsceneManager cutsceneManager, DiContainer diContainer)
        {
            _playerData = playerData;
            _inputReader = inputReader;

            _container = diContainer;

            if(_initializeStartItemsByCutscene) 
                cutsceneManager.OnFirstCutsceneEnded += InitializeStartItems;
        }

        private void Start()
        {
            if (!_initializeStartItemsByCutscene)
            {
                InitializeStartItems();
            }
        }

        [Button]
        private void InitializeStartItems()
        {
            foreach (var item in _startItems)
            {
                _playerData.GameItems.Add(item);
            }
        }

        private void Awake()
        {
            InitializeAllItems();
        }

        private void OnEnable()
        {
            _inputReader.OnUseItemInHandsInput += TryUseCurrentSelectedItem;
            _inputReader.OnScrollGameItemsInput += ScrollCurrentSelectedItemIndex;

            _playerData.GameItems.OnListCountChanged += InitializeItemFromPlayerData;
            _playerData.GameItems.OnItemRemoved += RemoveInitializedItem;
        }

        private void RemoveInitializedItem(GameItemSO removed)
        {
            for (var i = 0; i < _itemWithInstances.Count; i++)
            {
                var item = _itemWithInstances[i];
                if (item.GameItemSO == removed)
                {
                    if (_currentSelectedItemWithInstance == i) ScrollCurrentSelectedItemIndex(-1);
                    Destroy(item.SpawnedItem);
                    _itemWithInstances.RemoveAt(i);
                    break;
                }
            }
        }

        private void InitializeItemFromPlayerData(int index, GameItemSO newItem)
        {
            InitializeItemFromPlayerData(index);
        }

        private void OnDisable()
        {
            if (_inputReader != null)
            {
                _inputReader.OnUseItemInHandsInput -= TryUseCurrentSelectedItem;
                _inputReader.OnScrollGameItemsInput -= ScrollCurrentSelectedItemIndex;
            }

            OnSelectedItemChanged = null;
        }

        private void ScrollCurrentSelectedItemIndex(int side)
        {
            if (_itemWithInstances.Count == 0) return;
            if (!_canChangeSelectedItem) return;
            
            var oldSelectedValue = _currentSelectedItemWithInstance;
            _currentSelectedItemWithInstance = (int)Mathf.Repeat(_currentSelectedItemWithInstance + side,
                _itemWithInstances.Count);

            if (oldSelectedValue != _currentSelectedItemWithInstance)
            {
                _canChangeSelectedItem = false;
                
                UniTask.Void(async () => {
                    await UniTask.Delay(TimeSpan.FromSeconds(_delayBetweenScrollingToNextItem), DelayType.Realtime);
                    _canChangeSelectedItem = true;
                });
                
                _itemWithInstances[oldSelectedValue].SpawnedItem.SetActive(false);
                _itemWithInstances[_currentSelectedItemWithInstance].SpawnedItem.SetActive(true);
                
                OnSelectedItemChanged?.Invoke(_itemWithInstances[oldSelectedValue],
                    _itemWithInstances[_currentSelectedItemWithInstance]);
            }
        }
        
        [Button]
        private void InitializeAllItems()
        {
            for (var i = 0; i < _playerData.GameItems.Count; i++)
            {
                InitializeItemFromPlayerData(i, i == 0);
            }
        }

        private void InitializeItemFromPlayerData(int itemIndex, bool activeStateOfItem = false)
        {
            if (_playerData.GameItems.Count <= itemIndex)
            {
                LogHandler.LogError(this, nameof(itemIndex) + " greater than game items count!");
                return;
            }

            if (_itemWithInstances.Count == 0) activeStateOfItem = true;

            var element = new ItemWithInstance(_playerData.GameItems[itemIndex],
                _spawnedGameItemsParent, _container, activeStateOfItem);
            
            _itemWithInstances.Add(element);

            if (activeStateOfItem)
            {
                OnSelectedItemChanged?.Invoke(null, element);
            }
        }

        private void TryUseCurrentSelectedItem(bool isUseButtonPressed)
        {
            if (_itemWithInstances.Count == 0 && _currentSelectedItemWithInstance >= _itemWithInstances.Count)
            {
                LogHandler.LogWarning(this, "cant use item!");
                return;
            }
            _itemWithInstances[_currentSelectedItemWithInstance].UsableInstance.UseItem(isUseButtonPressed);
        }
        
        [Serializable]
        public class ItemWithInstance
        {
            public GameItemSO GameItemSO;
            public IUsableGameItem UsableInstance;
            public GameObject SpawnedItem;
            
            public ItemWithInstance(GameItemSO gameItemSO, Transform gameItemParent, 
                DiContainer container, bool activeStateOfSpawned = false)
            {
                GameItemSO = gameItemSO;
                SpawnedItem = GameItemSO.SpawnGameItemPrefab(gameItemParent, container);
                
                UsableInstance = GameItemSO.CreateInstance();
                UsableInstance ??= SpawnedItem.GetComponent<MonoUsableGameItem>();
                
                SpawnedItem.SetActive(activeStateOfSpawned);
            }
        }
    }
}