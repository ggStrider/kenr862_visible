using Definitions.Puzzle.Codes;
using Internal.Core.Audio;
using Internal.Core.Inputs;
using Internal.Core.Tools;
using Internal.Core.UI;
using Internal.Core.UI.Bases;
using Internal.Gameplay.Interact;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Internal.Gameplay.Puzzle
{
    public abstract class FourDigitCodePanel : MonoBehaviour, IInteractable, IEscClosable
    {
        [SerializeField, InlineEditor] private FourDigitCodeSO _code;

        [Space] [SerializeField] private AudioPlayer _onChangedNumber;
        
        [Space]
        [SerializeField, ReadOnly] private bool _alreadyUnlocked;

        private UFourDigitCodeEnterer _uiFourDigitCodeEnterer;
        private InputReader _inputReader;
        private EscManager _escManager;

        public int Priority { get; set; } = 0;

        private void Awake()
        {
            LogHandler.LogIfNull(this, _code, nameof(_code));
        }

        [Inject]
        private void Construct(InputReader inputReader, UFourDigitCodeEnterer uFourDigitCodeEnterer, 
            EscManager escManager)
        {
            _inputReader = inputReader;
            _uiFourDigitCodeEnterer = uFourDigitCodeEnterer;
            _escManager = escManager;
        }

        public void Interact(bool isInteractButtonPressed)
        {
            if (_alreadyUnlocked) return;
            if (!isInteractButtonPressed) return;
            EnterToUIPanel();
        }

        private void EnterToUIPanel()
        {
            _inputReader.EnableMapUI();
            _uiFourDigitCodeEnterer.gameObject.SetActive(true);
            _uiFourDigitCodeEnterer.ClearCode();
            
            _escManager.AddToStack(this);

            _inputReader.OnCodeSwitch += HandleDigitInput;
        }

        private void HandleDigitInput(Vector2 side)
        {
            // up-down code
            if (side.y != 0)
            {
                _uiFourDigitCodeEnterer.EnterDigit((int)Mathf.Sign(side.y));
                _onChangedNumber.PlayRandomShot();

                if (_uiFourDigitCodeEnterer.CurrentCode.Length >= 4)
                {
                    CheckCode();
                }
            }
            // code section left right
            else if (side.x != 0)
            {
                _uiFourDigitCodeEnterer.AddDeltaToCurrentSetDigitIndex((int)Mathf.Sign(side.x));
            }
        }

        private void CheckCode()
        {
            if (_uiFourDigitCodeEnterer.CurrentCode == _code)
            {
                _alreadyUnlocked = true;
                OnSuccessfulEntered();
                ReturnToPlayer();
            }
            else
            {
                OnWrongEntered();
            }

            // _uiFourDigitCodeEnterer.ClearCode();
        }

        private void ReturnToPlayer()
        {
            _uiFourDigitCodeEnterer.gameObject.SetActive(false);
            _inputReader.EnableMapPlayer();

            _inputReader.OnCodeSwitch -= HandleDigitInput;
        }

        protected abstract void OnSuccessfulEntered();
        protected virtual void OnWrongEntered() { } 
        
        public void Close()
        {
            ReturnToPlayer();
        }
    }
}