using System;
namespace Blue
{
    public static class ICanSubscribeEventExtension
    {
        private static ITypeEventSystem _eventSystem;
        public static void SetEventSystem(ITypeEventSystem eventSystem)
        {
            _eventSystem = eventSystem;
        }

        public static IUnSubscribe SubscribeEvent<T>(this ICanSubscribeEvent self, Action<T> onEvent) where T : IEvent
        {
           return _eventSystem.SubscribeEvent<T>(onEvent);
        }
        public static IChainEventUnSubscribe SubscribeChainEvent<T,K>(this ICanSubscribeEvent self,Action<K> onEvent) where T : IChainEvent where K:IEvent
        {
            return _eventSystem.SubscribeChainEvent<T,K>(onEvent);
        }
        public static void UnSubscribeEvent<T>(this ICanSubscribeEvent self, Action<T> onEvent) where T : IEvent
        {
            _eventSystem.UnSubscribeEvent<T>(onEvent);
        }
        public static void UnSubscribeChainEvent<T>(this ICanSubscribeEvent self) where T:IChainEvent
        {
            _eventSystem.UnSubscribeChainEvent<T>();
        }

        public static void UnSubscribeEventFromChainEvent<T, K>(this ICanSubscribeEvent self) where T : IChainEvent where K : IEvent
        {
            _eventSystem.UnSubscribeEventFromChainEvent<T, K>();
        }
    }
}
