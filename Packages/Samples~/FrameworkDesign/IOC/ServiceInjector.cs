using System;
namespace Blue
{
    /// <summary>
    /// Service 层注入
    /// </summary>
    public class ServiceInjector : AbstractArchitectureComponentInjector
    {
        private Type serviceType;

        /// <summary>
        /// 获取 IService 的类型
        /// </summary>
        public ServiceInjector():base(typeof(IService)) {
            serviceType = typeof(IService);
        }

        /// <summary>
        /// 根据类型获取注入的Object
        /// </summary>
        protected override object GetInjectObject(Type baseType)
        {
            object instance= architectureInstance.GetService(baseType);
            if (instance == null)
            {
                var interfaces = baseType.GetInterfaces();

                foreach (var type in interfaces)
                {
                    if (serviceType.IsAssignableFrom(type)&&!serviceType.Equals(type))
                    {
                        instance = architectureInstance.GetService(type);
                        if (instance != null)
                        {
                            break;
                        }
                    }
                }
            }
            return instance;
        }
    }
}
