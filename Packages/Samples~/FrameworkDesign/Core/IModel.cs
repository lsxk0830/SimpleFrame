namespace Blue
{
    /// <summary>
    /// Model 层接口
    /// </summary>
    public interface IModel:ICanInject,ICanGetUtility,ICanTriggerEvent
    {
        void OnInit();
    }
}
