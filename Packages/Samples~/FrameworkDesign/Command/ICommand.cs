namespace Blue
{
    public interface ICommand:ICanGetService,ICanGetModel,ICanGetUtility,ICanTriggerEvent,ICanSendCommand
    {
        void OnExcute();
    }
}
