using System;

namespace Blue
{
    /// <summary>
    /// Model 层注入
    /// </summary>
    public class ModelInjector : AbstractArchitectureComponentInjector
    {
        private Type modelType;

        /// <summary>
        /// 获取 IModel 的类型
        /// </summary>
        public ModelInjector() : base(typeof(IModel)) {
            modelType = typeof(IModel);
        }

        /// <summary>
        /// 根据类型获取注入的Object
        /// </summary>
        protected override object GetInjectObject(Type baseType)
        {
            object instance = architectureInstance.GetModel(baseType);
            if (instance == null)
            {
                var interfaces = baseType.GetInterfaces(); // 当在派生类中重写时，获取由当前 Type 实现或继承的所有接口
                foreach (var type in interfaces)
                {
                    if (modelType.IsAssignableFrom(type)&&!modelType.Equals(type)) // 确定指定类型 type 的实例是否能分配给当前类型的变量
                    {
                        instance = architectureInstance.GetModel(type);
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
