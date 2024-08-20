namespace Blue
{
    /// <summary>
    /// Service 层接口
    /// </summary>
    public interface IService : ICanInject,ICanGetService, ICanSendCommand,ICanGetModel, ICanGetUtility,ICanSubscribeEvent,ICanTriggerEvent,ICanSendQuery
    {
        void OnInit();
    }
}
