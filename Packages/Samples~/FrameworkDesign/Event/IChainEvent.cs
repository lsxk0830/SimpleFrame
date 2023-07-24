using System;
namespace Blue
{
    public interface IChainEvent : IEvent
    {
        //IChainEvent NextEvent<T>(Action<T> onEvent) where T : IEvent;
    }
}
