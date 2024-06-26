using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace SimpleFrame
{
    public static partial class ToolExtension
    {
        public static void AALoadAsync<T>(this object self, string key, Action<T> callback)
        {
            Addressables.LoadAssetAsync<T>(key).Completed += (result) =>
            {
                if (result.Status == AsyncOperationStatus.Succeeded)
                    callback?.Invoke(result.Result);
                else
                {
                    string errorMessage = result.OperationException != null ? result.OperationException.Message : "Unknown error";
                    Debug.LogError($"AA加载失败: 无法加载Key '{key}' 的资源. 错误: {errorMessage}");
                }
            };
        }
    }
}