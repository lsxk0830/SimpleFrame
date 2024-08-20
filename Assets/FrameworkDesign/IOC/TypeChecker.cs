using System;
using UnityEngine.Scripting;

namespace Blue
{
    /// <summary>
    /// 类型检查 --- Preserve属性
    /// 防止字节码剥离移除类、方法、字段或属性
    /// </summary>
    [Preserve]
    public class TypeChecker:Singleton<TypeChecker>
    {
        private Type canInjectBaseType;
        private Type controllerType;
        private Type modelType;
        private Type serviceType;
        private Type utilityType;
       // [RequiredMember]
        private TypeChecker()
        {
            canInjectBaseType = typeof(ICanInject);
            controllerType = typeof(IController);
            modelType = typeof(IModel);
            serviceType = typeof(IService);
            utilityType = typeof(IUtility);
        }

        /// <summary>
        /// 传入的类型是否来自ICanInject类型
        /// </summary>
        /// <param name="type">传入的类型</param>
        public bool IsCanInject(Type type)
        {
            /*
            TypeA.IsAssignableFrom
            如果TypeA和TypeB类型一样则返回true；
            如果TypeA是TypeB的父类则返回true;
            如果TypeB实现了接口TypeA则返回true;
            */
            return canInjectBaseType.IsAssignableFrom(type); // 确定指定类型 type 的实例是否能分配给当前类型的变量
        }

        /// <summary>
        /// 传入的类型是否来自IController类型
        /// </summary>
        /// <param name="type">传入的类型</param>
        public bool IsController(Type type)
        {
            return controllerType.IsAssignableFrom(type);
        }

        /// <summary>
        /// 传入的类型是否来自IModel类型
        /// </summary>
        /// <param name="type">传入的类型</param>
        public bool IsModel(Type type)
        {
            return modelType.IsAssignableFrom(type);
        }

        /// <summary>
        /// 传入的类型是否来自IService类型
        /// </summary>
        /// <param name="type">传入的类型</param>
        public bool IsService(Type type)
        {
            return serviceType.IsAssignableFrom(type);
        }

        /// <summary>
        /// 传入的类型是否来自IUtility类型
        /// </summary>
        /// <param name="type">传入的类型</param>
        public bool IsUtility(Type type)
        {
            return utilityType.IsAssignableFrom(type);
        }
    }
}
