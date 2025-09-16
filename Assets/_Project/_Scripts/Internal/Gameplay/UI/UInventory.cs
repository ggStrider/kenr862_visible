using System;
using System.Threading;
using Core.DataModel;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using Tools;
using UnityEngine;
using Zenject;

namespace Internal.Gameplay.UI
{
    public class UInventory : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _itemNameLabel;
        [SerializeField] private TextMeshProUGUI _itemDescriptionLabel;
        
        [SerializeField, Min(0.01f)] private float _tweenDuration = 1f;
        [SerializeField] private float _waitUntilFadeItemNameLabel = 1.5f;
        
        private Inventory _inventory;

        private CancellationTokenSource _changeColorItemNameCts;
        
        [Inject]
        private void Construct(Inventory inventory)
        {
            _inventory = inventory;
        }

        private void OnEnable()
        {
            _inventory.OnSelectedItemChanged += OnCurrentItemChanged;
        }

        private void OnDisable()
        {
            if (_inventory)
            {
                _inventory.OnSelectedItemChanged -= OnCurrentItemChanged;
            }
        }

        private void OnCurrentItemChanged(Inventory.ItemWithInstance oldItem, Inventory.ItemWithInstance newItem)
        {
            _itemNameLabel.DOKill();
            _itemDescriptionLabel.DOKill();
            
            MyUniTaskExtensions.SafeCancelAndCleanToken(ref _changeColorItemNameCts, true);
            _itemNameLabel.text = newItem.GameItemSO.ItemName;
            _itemDescriptionLabel.text = newItem.GameItemSO.ItemDescription;

            var mainColor = _itemNameLabel.color;
            mainColor.a = 1;
            _itemNameLabel.color = mainColor;

            var descriptionColor = _itemDescriptionLabel.color;
            descriptionColor.a = 1;
            _itemDescriptionLabel.color = descriptionColor;
            
            WaitAndChangeColorOfItemNameAndDescriptionLabel().Forget();
        }

        private async UniTaskVoid WaitAndChangeColorOfItemNameAndDescriptionLabel()
        {
            try
            {
                await UniTask.WaitForSeconds(_waitUntilFadeItemNameLabel, 
                    cancellationToken: _changeColorItemNameCts.Token, ignoreTimeScale: true);

                var targetNameColor = _itemNameLabel.color;
                targetNameColor.a = 0;
                _itemNameLabel.DOColor(targetNameColor, _tweenDuration).SetUpdate(true);
                
                
                var targetDescriptionColor = _itemDescriptionLabel.color;
                targetDescriptionColor.a = 0;
                _itemDescriptionLabel.DOColor(targetDescriptionColor, _tweenDuration).SetUpdate(true);
            }
            catch (OperationCanceledException)
            {
            }
        }
    }
}