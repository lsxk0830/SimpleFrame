namespace Blue
{
    public class CommandSystem : SingletonMonobehaviour<CommandSystem>
    {
        /// <summary>
        /// 发送命令
        /// </summary>
        public void Send(ICommand command)
        {
            this.SendCommand(command);
        }
    }
}