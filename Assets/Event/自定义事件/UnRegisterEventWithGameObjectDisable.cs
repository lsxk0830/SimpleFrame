using System;
using UnityEngine;

namespace SimpleFrame
{
    /// <summary>
    /// 指定物体OnEnable时注册事件、OnDisable取消注册事件
    /// </summary>
    public class UnRegisterEventWithGameObjectDisable : MonoBehaviour
    {
        private Action RegisterEvents;
        private Action UnRegisterEvents;
        private EventIOC Global;

        public void Init(EventIOC EventIOC)
        {
            Global = EventIOC;
        }

        public void AddUnRegisterEventAction<T>(Action<T> action) where T : IEvent
        {
            RegisterEvents += () =>
            {
                Global.RegisterEvent<T>(action);
            };

            UnRegisterEvents += () =>
            {
                Global.UnRegisterEvent<T>(action);
            };
        }

        private void OnEnable()
        {
            this.Log($"Action:{RegisterEvents == null}");
            RegisterEvents?.Invoke();
        }

        private void OnDisable()
        {
            UnRegisterEvents?.Invoke();
        }
    }
}