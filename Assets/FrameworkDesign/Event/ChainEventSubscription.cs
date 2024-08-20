using System;
using System.Collections.Generic;


namespace Blue {
    public class ChainEventSubscription : IChainEventSubscription
    {
        private Dictionary<Type, int> actionHashPool;
        private Type mChainEventType;
        public ChainEventSubscription(Type chainEventType)
        {
            mChainEventType = chainEventType;
        }
        public int SubscribeCount => actionHashPool.Count;

        public IChainEventUnSubscribe Subscribe(Type eventType,int actionHashCode,Action onOnSubscribe)
        {
            actionHashPool.TryAdd(eventType, actionHashCode);
            return new DefaultChainEventUnSubcribe(mChainEventType, () => { onOnSubscribe?.Invoke(); UnSubscribe(eventType); });
        }
        public bool IsSubscribed(Type eventType)
        {
            return actionHashPool.ContainsKey(eventType);
        }
        public int GetActionHashCode(Type eventType)
        {
            return actionHashPool[eventType];
        }
        public List<Type> GetEventTypeList()
        {
            List<Type> valueList = new List<Type>(actionHashPool.Count);
            valueList.AddRange(actionHashPool.Keys);
            return valueList;
        }

        public bool UnSubscribe(Type eventType)
        {
            return actionHashPool.Remove(eventType);
        }
    }
}
