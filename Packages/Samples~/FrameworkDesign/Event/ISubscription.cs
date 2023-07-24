namespace Blue
{
    /// <summary>
    /// 订阅事件的接口
    /// </summary>
    public interface ISubscription
    {
        int SubscribeCount { get; }
        IUnSubscribe Subscribe(EventToActionAdapter adapter);
        bool UnSubscribe(int hashCode);
        void TriggerEvent(IEvent e);
    }
}
