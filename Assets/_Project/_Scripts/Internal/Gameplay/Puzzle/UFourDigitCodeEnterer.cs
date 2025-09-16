using System;
using TMPro;
using UnityEngine;

namespace Internal.Gameplay.Puzzle
{
    public class UFourDigitCodeEnterer : MonoBehaviour
    {
        [SerializeField] private CodeToEnterContainer _firstDigit;
        [SerializeField] private CodeToEnterContainer _secondDigit;
        [SerializeField] private CodeToEnterContainer _thirdDigit;
        [SerializeField] private CodeToEnterContainer _fourthDigit;

        private string _currentCode = "";
        private int _currentIndexToSet;

        private void OnEnable()
        {
            AddDeltaToCurrentSetDigitIndex(0);
        }

        public void EnterDigit(int side)
        {
            var nextValue = 0;

            switch (_currentIndexToSet)
            {
                case 0:
                    nextValue = (int)Mathf.Repeat(int.Parse(_firstDigit.Digit.text) + side, 10);
                    _firstDigit.Digit.text = nextValue.ToString();
                    break;
                case 1:
                    nextValue = (int)Mathf.Repeat(int.Parse(_secondDigit.Digit.text) + side, 10);
                    _secondDigit.Digit.text = nextValue.ToString();
                    break;
                case 2:
                    nextValue = (int)Mathf.Repeat(int.Parse(_thirdDigit.Digit.text) + side, 10);
                    _thirdDigit.Digit.text = nextValue.ToString();
                    break;
                case 3:
                    nextValue = (int)Mathf.Repeat(int.Parse(_fourthDigit.Digit.text) + side, 10);
                    _fourthDigit.Digit.text = nextValue.ToString();
                    break;
            }

            _currentCode = $"{_firstDigit.Digit.text}{_secondDigit.Digit.text}{_thirdDigit.Digit.text}{_fourthDigit.Digit.text}";
            UpdateUI();
        }

        public void AddDeltaToCurrentSetDigitIndex(int delta)
        {
            var before = GetContainerByIndex(_currentIndexToSet);
            before.Selector.SetActive(false);

            _currentIndexToSet = (int)Mathf.Repeat(_currentIndexToSet + delta, 4);
            var current = GetContainerByIndex(_currentIndexToSet);
            current.Selector.SetActive(true);
        }

        private CodeToEnterContainer GetContainerByIndex(int index)
        {
            if (index == 0)
            {
                return _firstDigit;
            }
            else if (index == 1)
            {
                return _secondDigit;
            }
            else if (index == 2)
            {
                return _thirdDigit;
            }
            return _fourthDigit;
        }

        private void UpdateUI()
        {
            _firstDigit.Digit.text = _currentCode.Length > 0 ? _currentCode[0].ToString() : "0";
            _secondDigit.Digit.text = _currentCode.Length > 1 ? _currentCode[1].ToString() : "0";
            _thirdDigit.Digit.text = _currentCode.Length > 2 ? _currentCode[2].ToString() : "0";
            _fourthDigit.Digit.text = _currentCode.Length > 3 ? _currentCode[3].ToString() : "0";
        }

        public string CurrentCode => _currentCode;

        public void ClearCode()
        {
            _currentCode = "";
            UpdateUI();
        }

        [Serializable]
        public class CodeToEnterContainer
        {
            [field: SerializeField] public TextMeshProUGUI Digit { get; private set; }
            [field: SerializeField] public GameObject Selector { get; private set; }
        }
    }
}