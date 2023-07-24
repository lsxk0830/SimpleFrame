using System;
using System.Collections.Generic;
namespace Blue
{
    public class TypeEventSubscription : ISubscription
    {
        private Action<IEvent> mOnEvent;
        private int subscribeCount;
        private Type mEventType;
        private Dictionary<int, EventToActionAdapter> adapterPool = new Dictionary<int, EventToActionAdapter>();
        public TypeEventSubscription(Type eventType) 
        {
            mEventType = eventType;
        }
        public int SubscribeCount { get => subscribeCount;}
        public IUnSubscribe Subscribe(EventToActionAdapter adapter)
        {
            try
            {
                adapterPool.Add(adapter.GetActionHashCode(), adapter);
                mOnEvent += adapter.GetAdapterAction();
                subscribeCount++;
                return new TypeEventSystemUnSubscribe(() => { UnSubscribe(adapter.GetActionHashCode()); });
            }
            catch (ArgumentException)
            {
                //LogUtil.LogError("Event subscription duplicated on "+mEventType+",please make sure you have unsubscribe it before re-subscribe!");
                throw;
            }
        }
        public void TriggerEvent(IEvent e)
        {
            mOnEvent?.Invoke(e);
        }

        public bool UnSubscribe(int hashCode)
        {
            if (adapterPool.TryGetValue(hashCode, out var adapter)) 
            {
                mOnEvent -= adapter.GetAdapterAction();
                subscribeCount--;
                adapter.Clear();
                adapterPool.Remove(hashCode);
                return true;
            }
            return false;
        }
    }
}
