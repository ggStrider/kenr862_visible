using System;
using Cysharp.Threading.Tasks;

namespace Internal.Core.Tools
{
    public static class CooldownRunner
    {
        public static void Run(float cooldown, Action action)
        {
            UniTask.Void(async () => {
                await UniTask.Delay(TimeSpan.FromSeconds(cooldown));
                action?.Invoke();
            });

        }
    }
}