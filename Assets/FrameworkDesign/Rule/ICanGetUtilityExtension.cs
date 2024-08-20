namespace Blue
{

    public static class ICanGetUtilityExtension
    {
        private static IArchitecture _architecture;
        public static void SetArchitecture(IArchitecture architecture)
        {
            _architecture = architecture;
        }
        public static T GetUtility<T>(this ICanGetUtility self) where T : IUtility
        {
            return _architecture.GetUtility<T>();
        }
    }
}
