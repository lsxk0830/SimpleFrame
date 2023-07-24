using System.Collections;
using UnityEngine;

namespace Blue
{
    public class CoroutineSystem : SingletonMonobehaviour<CoroutineSystem>
    {
        /// <summary>
        /// 启动一个协程
        /// </summary>
        public Coroutine Start_Coroutine(IEnumerator coroutine)
        {
            return StartCoroutine(coroutine);
        }

        /// <summary>
        /// 停止一个协程序
        /// </summary>
        public void Stop_Coroutine(Coroutine routine)
        {
            StopCoroutine(routine);
        }
    }
}