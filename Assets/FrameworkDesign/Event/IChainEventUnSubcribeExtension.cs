using System;
using UnityEngine;
namespace Blue
{
    public static class IChainEventUnSubcribeExtension
    {
        private static ITypeEventSystem _typeEventSystem;
        public static void SetEventSystem(ITypeEventSystem typeEventSystem) 
        {
            _typeEventSystem = typeEventSystem;
        }
        public static void UnSubscribeAllEventOnGameobjectDestroyed(this IChainEventUnSubscribe unSubscribe, GameObject gameObject)
        {
            var trigger = gameObject.GetComponent<UnSubscribeChainEventOnDestroyTrigger>();
            if (!trigger)
            {
                trigger = gameObject.AddComponent<UnSubscribeChainEventOnDestroyTrigger>();
            }
            trigger.AddUnSubscribe(unSubscribe);
        }
        public static IChainEventUnSubscribe NextEvent<T>(this IChainEventUnSubscribe self,Action<T> onEvent) where T:IEvent
        {
            return _typeEventSystem.SubscribeChainEvent(self.GetChainEventType(),onEvent);
        }

        public static void UnSubscribeAllEventsOnChain(this IChainEventUnSubscribe self) 
        {
            _typeEventSystem.UnSubscribeChainEvent(self.GetChainEventType());
        }
    }
}
