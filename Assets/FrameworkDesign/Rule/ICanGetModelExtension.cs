namespace Blue
{
    /// <summary>
    /// ICanGetModel接口的扩展类
    /// ① 设置Architecture
    /// ② 扩展方法---GetModel
    /// </summary>
    public static class ICanGetModelExtension
    {
        private static IArchitecture _architecture;
        public static void SetArchitecture(IArchitecture architecture)
        {
            _architecture = architecture;
        }

        public static T GetModel<T>(this ICanGetModel self)where T:class,IModel
        {
            return _architecture.GetModel<T>();
        }
    }
}
