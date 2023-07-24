namespace Blue
{
    public static class IArchitectureModuleExtension
    {
        private static IArchitecture _architecture;
        public static void SetArchitecture(IArchitecture architecture)
        {
            _architecture = architecture;
        }
        public static void RegisterModel<T>(this IArchitectureModule self) where T : IModel, new()
        {
            _architecture.RegisterModel<T>();
        }
        public static void RegisterModel<T>(this IArchitectureModule self,T modelInstance) where T : IModel
        {
            _architecture.RegisterModel<T>(modelInstance);
        }

        public static void RegisterService<T>(this IArchitectureModule self) where T:IService,new()
        {
            _architecture.RegisterService<T>();
        }
        public static void RegisterService<T>(this IArchitectureModule self,T serviceInstance) where T : IService
        {
            _architecture.RegisterService<T>(serviceInstance);
        }
        public static void RegisterUtility<T>(this IArchitectureModule self) where T : IUtility, new()
        {
            _architecture.RegisterUtility<T>();
        }
        public static void RegisterUtility<T>(this IArchitectureModule self, T utilityInstance) where T : IUtility
        {
            _architecture.RegisterUtility<T>(utilityInstance);
        }
    }
}
