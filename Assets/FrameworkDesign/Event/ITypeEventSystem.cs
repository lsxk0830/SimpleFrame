using System;
namespace Blue
{
    /// <summary>
    /// 事件系统接口
    /// ① 注册事件、注册链事件
    /// ② 订阅事件、订阅链事件
    /// ③ 取消订阅事件、取消订阅链事件
    /// ④ 取消订阅链事件中的事件
    /// </summary>
    public interface ITypeEventSystem
    {
        void TriggerEvent<T>() where T : IEvent, new();
        void TriggerEvent<T>(T e) where T : IEvent;
        void TriggerChainEvent<T>(params IEvent[] events) where T : IChainEvent;
        void TriggerChainEvent<T>() where T : IChainEvent;

        IUnSubscribe SubscribeEvent<T>(Action<T> onEvent) where T : IEvent;
        IChainEventUnSubscribe SubscribeChainEvent<T, K>(Action<K> onEvent) where T : IChainEvent where K : IEvent;
        IChainEventUnSubscribe SubscribeChainEvent<K>(Type chainEventType,Action<K> onEvent) where K : IEvent;

        void UnSubscribeEvent<T>(Action<T> onEvent) where T : IEvent;
        void UnSubscribeChainEvent<T>() where T : IChainEvent;
        void UnSubscribeChainEvent(Type chainEventType);

        void UnSubscribeEventFromChainEvent<T, K>() where T : IChainEvent where K : IEvent;
    }
}
