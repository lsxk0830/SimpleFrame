namespace Blue
{

    public static class ICanGetServiceExtension
    {
        private static IArchitecture _architecture;
        public static void SetArchitecture(IArchitecture architecture)
        {
            _architecture = architecture;
        }

        public static T GetService<T>(this ICanGetService self) where T : IService
        {
            return _architecture.GetService<T>();
        }
    }
}
