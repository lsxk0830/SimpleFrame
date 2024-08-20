namespace Blue
{
    /// <summary>
    /// 默认Command 处理类
    /// </summary>
    public class DefaultCommandHandler : ICommandHandler
    {
        public void ExcuteCommand(ICommand command)
        {
            command.OnExcute();
        }

        public void ExcuteCommand<T>() where T : ICommand, new()
        {
            ExcuteCommand(new T());
        }
    }
}
