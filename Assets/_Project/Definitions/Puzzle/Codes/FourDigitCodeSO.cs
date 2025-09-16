using Internal.Core.Extensions;
using UnityEngine;

namespace Definitions.Puzzle.Codes
{
    [CreateAssetMenu(fileName = "New Four Digit", menuName = StaticKeys.PROJECT_NAME + 
                                                             "/Puzzle/Four Digit Code")]
    public class FourDigitCodeSO : ScriptableObject
    {
        [field: SerializeField] public char FirstDigit = '0';
        [field: SerializeField] public char SecondDigit = '0';
        [field: SerializeField] public char ThirdDigit = '0';
        [field: SerializeField] public char FourthDigit = '0';

        public string FourDigitCode => FirstDigit.ToString() + SecondDigit.ToString() + ThirdDigit.ToString() + FourthDigit.ToString();

        private void OnValidate()
        {
            CharExtensions.ValidateIntoDigit(ref FirstDigit);
            CharExtensions.ValidateIntoDigit(ref SecondDigit);
            CharExtensions.ValidateIntoDigit(ref ThirdDigit);
            CharExtensions.ValidateIntoDigit(ref FourthDigit);
        }

        public static implicit operator string(FourDigitCodeSO code)
        {
            return code.FourDigitCode;
        }
    }
}