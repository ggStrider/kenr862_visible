using UnityEngine;

namespace Internal.Core.Tools
{
    public static class LogHandler
    {
        public static void LogError(object sender, string message)
        {
            Debug.LogError($"[{sender.GetType().Name}] {message}");
        }

        public static void LogNull<T>(object sender, T nullObject)
        {
            LogError(sender, typeof(T).Name + " is null!");
        }
        
        public static void LogNull(object sender, string fieldName)
        {
            LogError(sender, fieldName + " is null!");
        }
        
        public static void LogIfNull(object sender, object possiblyNull, string fieldName)
        {
            if (possiblyNull != null) return;
            LogError(sender, fieldName + " is null!");
        }

        public static void LogWarning(object sender, string text)
        {
            Debug.LogWarning($"[{sender.GetType().Name}] {text}");
        }

        public static void ThrowIfNull(object sender, object possiblyNull, string fieldName)
        {
            if(possiblyNull != null) return;
            throw new MissingComponentException($"[{sender.GetType()}] {fieldName} is null!");
        }
        
        public static void ThrowIfNull<T>(object sender, T possiblyNull)
        {
            if(possiblyNull != null) return;
            throw new MissingComponentException($"[{sender.GetType()}] {typeof(T).Name} is null!");
        }
    }
}