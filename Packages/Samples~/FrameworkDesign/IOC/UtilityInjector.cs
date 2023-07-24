using System;
namespace Blue
{
    /// <summary>
    /// Utility 层注入
    /// </summary>
    public class UtilityInjector : AbstractArchitectureComponentInjector
    {
        private Type utilityType;

        /// <summary>
        /// 获取 IUtility 的类型
        /// </summary>
        public UtilityInjector():base(typeof(IUtility))
        {
            utilityType = typeof(IUtility);
        }

        /// <summary>
        /// 根据类型获取注入的Object
        /// </summary>
        protected override object GetInjectObject(Type baseType)
        {
            object instance = architectureInstance.GetUtility(baseType);
            if (instance == null)
            {
                var interfaces = baseType.GetInterfaces();
                foreach (var type in interfaces)
                {
                    if (utilityType.IsAssignableFrom(type)&&!utilityType.Equals(type))
                    {
                        instance = architectureInstance.GetUtility(type);
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
