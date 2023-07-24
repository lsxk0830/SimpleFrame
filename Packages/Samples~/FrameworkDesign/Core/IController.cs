namespace Blue
{
    /// <summary>
    /// Controller 层接口
    /// </summary>
    public interface IController :ICanInject,ICanGetService,ICanGetModel,ICanGetUtility,ICanSendCommand,ICanSubscribeEvent,ICanSendQuery
    {
    }
}
