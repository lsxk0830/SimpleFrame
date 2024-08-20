namespace Blue
{

    public static class ICanSendCommandExtension
    {
        private static ICommandHandler _commandHandler;

        public static void SetCommandHandler(ICommandHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }

        public static void SendCommand<T>(this ICanSendCommand self) where T : ICommand, new()
        {
            _commandHandler.ExcuteCommand<T>();
        }
        public static void SendCommand<T>(this ICanSendCommand self, T command) where T : ICommand
        {
            _commandHandler.ExcuteCommand(command);
        }
    }
}
