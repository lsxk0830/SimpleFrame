namespace Blue
{

    public static class ICanTriggerEventExtension
    {
        private static ITypeEventSystem _eventSystem;
        public static void SetEventSystem(ITypeEventSystem eventSystem)
        {
            _eventSystem = eventSystem;
        }

        public static void TriggerEvent<T>(this ICanTriggerEvent self) where T:IEvent,new()
        {
            _eventSystem.TriggerEvent<T>();
        }
        public static void TriggerEvent<T>(this ICanTriggerEvent self,T e) where T : IEvent
        {
            _eventSystem.TriggerEvent(e);
        }
        public static void TriggerChainEvent<T>(this ICanTriggerEvent self,params IEvent[] events) where T : IChainEvent
        {
            _eventSystem.TriggerChainEvent<T>(events);
        }
        public static void TriggerChainEvent<T>(this ICanTriggerEvent self) where T : IChainEvent
        {
            _eventSystem.TriggerChainEvent<T>();
        }
    }
}
