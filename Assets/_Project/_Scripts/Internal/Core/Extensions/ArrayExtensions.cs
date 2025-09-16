using UnityEngine;

namespace Internal.Core.Extensions
{
    public static class ArrayExtensions
    {
        public static T GetRandomElement<T>(this T[] array)
        {
            var randomIndex = Random.Range(0, array.Length);
            return array[randomIndex];
        }

        public static bool NotNullAndHasElements<T>(this T[] array)
        {
            return array != null && array.Length > 0;
        }
    }
}