using Internal.Gameplay.GameItems;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Definitions.GameItems.Scripts
{
    public abstract class GameItemSO : ScriptableObject
    {
        // [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public string ItemName = "A Fish";
        [field: SerializeField] public string ItemDescription = "I'm not a Fish";
        
        [Space] 
        [SerializeField] private GameObject _prefab;

        [SerializeField] private Vector3 _positionOffset = Vector3.zero;
        [SerializeField] private Vector3 _localRotation = Vector3.zero;

        [Space] 
        [SerializeField] private bool _useOriginalSize = true;
        
        [HideIf(nameof(_useOriginalSize))]
        [SerializeField] private Vector3 _size = Vector3.one;

        public abstract IUsableGameItem CreateInstance();

        public GameObject SpawnGameItemPrefab(Transform parent, DiContainer container)
        {
            // var item = Object.Instantiate(_prefab, parent, false);
            var item = container.InstantiatePrefab(_prefab, parent);
            
            if (!_useOriginalSize) item.transform.localScale = _size;
            item.transform.localPosition = _positionOffset;
            item.transform.localRotation = Quaternion.Euler(_localRotation);
            
            item.layer = StaticKeys.GameItemInHand_LAYER_INDEX;
            foreach (var child in item.GetComponentsInChildren<Transform>(true))
            {
                child.gameObject.layer = StaticKeys.GameItemInHand_LAYER_INDEX;
                if (child.TryGetComponent<Collider>(out var collider))
                {
                    collider.enabled = false;
                }
            }
            
            return item;
        }
    }
}