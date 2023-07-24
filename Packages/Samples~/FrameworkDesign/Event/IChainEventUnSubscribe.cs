using System;
namespace Blue
{
    /// <summary>
    /// 链事件取消订阅事件的接口
    /// </summary>
    public interface IChainEventUnSubscribe:IUnSubscribe
    {
        Type GetChainEventType();
    }
}
