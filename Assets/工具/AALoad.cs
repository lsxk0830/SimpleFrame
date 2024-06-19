using System;
using UnityEngine.AddressableAssets;

namespace SimpleFrame
{
    public static partial class ToolExtension
    {
        public static void AALoad<T>(this object self, string key, Action<T> callback)
        {
            Addressables.LoadAssetAsync<T>(key).Completed += (result) =>
            {
                T t = result.Result;
                callback?.Invoke(t);
            };

        }
    }
}