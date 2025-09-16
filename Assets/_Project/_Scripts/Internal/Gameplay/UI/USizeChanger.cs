using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Internal.Gameplay.Hover
{
    public class USizeChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        [SerializeField] private float _tweenDuration = 0.15f;
        
        [Space]
        [SerializeField] private Vector3 _defaultSize;
        [SerializeField] private Vector3 _onHoverSize;
        [SerializeField] private Vector3 _onClickSize;
            
        public void OnPointerEnter(PointerEventData eventData)
        {
            ChangeSize(_onHoverSize);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ChangeSize(_defaultSize);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            ChangeSize(_onClickSize);
        }

        private void ChangeSize(Vector3 to)
        {
            transform?.DOKill();
            transform.DOScale(to, _tweenDuration);
        }
        
        #if UNITY_EDITOR
        private void Reset()
        {
            if (_defaultSize == Vector3.zero)
            {
                _defaultSize = transform.localScale;
            }

            if (_onHoverSize == Vector3.zero)
            {
                _onHoverSize = _defaultSize + new Vector3(0.15f, 0.15f, 0.15f);
            }

            if (_onClickSize == Vector3.zero)
            {
                _onClickSize = _defaultSize - new Vector3(0.15f, 0.15f, 0.15f);
            }
        }
        #endif
    }
}