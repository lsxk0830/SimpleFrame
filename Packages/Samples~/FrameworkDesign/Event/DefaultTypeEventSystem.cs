using System;
using System.Collections.Generic;

namespace Blue
{
    /// <summary>
    /// 默认事件系统类，实现ITypeEventSystem
    /// </summary>
    public class DefaultTypeEventSystem : ITypeEventSystem
    {
        private Dictionary<Type, ISubscription> eventSubscriptionPool = new Dictionary<Type, ISubscription>(128);
        private Dictionary<Type, IChainEventSubscription> chainEventPool = new Dictionary<Type, IChainEventSubscription>(8);
        public IUnSubscribe SubscribeEvent<T>(Action<T> onEvent) where T : IEvent
        {
            Type t = typeof(T);
            ISubscription subscription;
            var adapter = new EventToActionAdapter(onEvent.GetHashCode(),(ievent) => { onEvent?.Invoke((T)ievent); });
            if (!eventSubscriptionPool.TryGetValue(t, out subscription))
            {
                subscription = new TypeEventSubscription(t);
                eventSubscriptionPool.Add(t, subscription);
            }
            return subscription.Subscribe(adapter);
        }

        public IChainEventUnSubscribe SubscribeChainEvent<T,K>(Action<K> onEvent) where T : IChainEvent where K:IEvent
        {
            Type chainEventType = typeof(T);
            return SubscribeChainEvent(chainEventType,onEvent);
        }
        public IChainEventUnSubscribe SubscribeChainEvent<K>(Type chainEventType, Action<K> onEvent) where K : IEvent 
        {
            Type eventType = typeof(K);
            IChainEventSubscription chainSubscription;
            if (!chainEventPool.TryGetValue(chainEventType, out chainSubscription))
            {
                chainSubscription = new ChainEventSubscription(chainEventType);
                chainEventPool.Add(chainEventType, chainSubscription);
            }
            if (chainSubscription.IsSubscribed(eventType))
            {
                UnSubscribeEvent(eventType, chainSubscription.GetActionHashCode(eventType));
                chainSubscription.UnSubscribe(eventType);
            }
            ISubscription eventSubscription;
            var adapter = new EventToActionAdapter(onEvent.GetHashCode(), (ievent) => { onEvent?.Invoke((K)ievent); });
            if (!eventSubscriptionPool.TryGetValue(eventType, out eventSubscription))
            {
                eventSubscription = new TypeEventSubscription(eventType);
                eventSubscriptionPool.Add(eventType, eventSubscription);
            }
            eventSubscription.Subscribe(adapter);
            return chainSubscription.Subscribe(eventType, onEvent.GetHashCode(),()=> { eventSubscription.UnSubscribe(onEvent.GetHashCode()); });
        }
        public void TriggerEvent<T>() where T : IEvent, new()
        {
            TriggerEvent(new T());
        }

        public void TriggerEvent<T>(T e) where T : IEvent
        {
            if (eventSubscriptionPool.TryGetValue(e.GetType(), out ISubscription subscription))
            {
                subscription.TriggerEvent(e);
            }
        }

        public void TriggerChainEvent<T>(params IEvent[] events) where T:IChainEvent
        {
            IChainEventSubscription chainSubscription;
            Type chainEventType = typeof(T);
            if (chainEventPool.TryGetValue(chainEventType, out chainSubscription))
            {
                Dictionary<Type, IEvent> eventInstanceMap = new Dictionary<Type, IEvent>(events.Length);
                foreach (var mEvent in events)
                {
                    eventInstanceMap.TryAdd(mEvent.GetType(),mEvent);
                }
                List<Type> chainEventTypeList=chainSubscription.GetEventTypeList();
                foreach (var eventType in chainEventTypeList)
                {
                    IEvent eventInstance;
                    if (!eventInstanceMap.TryGetValue(eventType, out eventInstance))
                    {
                        eventInstance = (IEvent)Activator.CreateInstance(eventType);
                    }
                    TriggerEvent(eventInstance);
                }
            }
        }
        public void TriggerChainEvent<T>() where T : IChainEvent
        {
            IChainEventSubscription chainSubscription;
            Type chainEventType = typeof(T);
            if (chainEventPool.TryGetValue(chainEventType, out chainSubscription))
            {
                List<Type> chainEventTypeList = chainSubscription.GetEventTypeList();
                foreach (var eventType in chainEventTypeList)
                {
                    TriggerEvent((IEvent)Activator.CreateInstance(eventType));
                }
            }
        }

        public void UnSubscribeEvent<T>(Action<T> onEvent) where T : IEvent
        {
            Type t = typeof(T);
            UnSubscribeEvent(t, onEvent.GetHashCode());
        }
        public void UnSubscribeChainEvent<T>() where T:IChainEvent
        {
            Type chainEventType = typeof(T);
            UnSubscribeChainEvent(chainEventType);
        }
        public void UnSubscribeChainEvent(Type chainEventType)
        {
            IChainEventSubscription chainSubscription;
            if (chainEventPool.TryGetValue(chainEventType, out chainSubscription))
            {
                List<Type> chainEventTypeList= chainSubscription.GetEventTypeList();
                foreach (var eventType in chainEventTypeList)
                {
                    UnSubscribeEvent(eventType,chainSubscription.GetActionHashCode(eventType));
                    chainSubscription.UnSubscribe(eventType);
                }
                chainEventPool.Remove(chainEventType);
            }
        }
        public void UnSubscribeEventFromChainEvent<T, K>() where T : IChainEvent where K : IEvent
        {
            IChainEventSubscription chainSubscription;
            Type chainEventType = typeof(T);
            if (chainEventPool.TryGetValue(chainEventType, out chainSubscription))
            {
                Type eventType = typeof(K);
                if (chainSubscription.UnSubscribe(eventType))
                {
                    UnSubscribeEvent(eventType,chainSubscription.GetActionHashCode(eventType));
                }
                if (chainSubscription.SubscribeCount == 0)
                {
                    chainEventPool.Remove(chainEventType);
                }
            }
        }
        private bool UnSubscribeEvent(Type eventType, int hash)
        {
            bool result = false;
            if (eventSubscriptionPool.TryGetValue(eventType, out ISubscription subscription))
            {
                result=subscription.UnSubscribe(hash);
                if (subscription.SubscribeCount <= 0)
                {
                    eventSubscriptionPool.Remove(eventType);
                }
            }
            return result;
        }
    }
}
