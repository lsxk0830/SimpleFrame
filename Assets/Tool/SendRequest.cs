using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace SimpleFrame
{
    public static partial class ToolExtension
    {
        public static IEnumerator SendRequest_Get(this object obj, string url, System.Action<string> callback)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                yield return webRequest.SendWebRequest();

                if (webRequest.result != UnityWebRequest.Result.Success)
                    Debug.LogError($"Error: {webRequest.error}");
                else
                    callback?.Invoke(webRequest.downloadHandler.text);
            }
        }
    }
}