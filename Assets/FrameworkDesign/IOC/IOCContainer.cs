using System.Collections.Generic;
using System;

namespace Blue
{
    /// <summary>
    /// IOC容器---根据类型存储Object
    /// </summary>
    public class IOCContainer
    {
        private Dictionary<Type, object> container = new Dictionary<Type, object>();

        /// <summary>
        /// 将实例注入IOC容器
        /// </summary>
        public void Register<T>() where T:new()
        {
            Register(new T());
        }

        /// <summary>
        /// 将实例注入IOC容器
        /// </summary>
        public void Register<T>(T instance)
        {
            Type t = typeof(T);
            if (container.ContainsKey(t))
            {
                container[t] = instance;
            }
            else
            {
                container.Add(t,instance);
            }
        }

        /// <summary>
        /// 根据类型从IOC容器获取对象
        /// </summary>
        public object Get(Type type)
        {
            if (container.TryGetValue(type,out object instance))
            {
                return instance;
            }
            return default;
        }

        /// <summary>
        /// 从IOC容器获取指定的实例
        /// </summary>
        public T Get<T>()
        {
            Type t = typeof(T);
            if (container.TryGetValue(t, out object instance))
            {
                return (T)instance;
            }
            else
            {
                throw new Exception("Can Not Find Instance of type "+t.FullName+",Please Call Register At First!");
            }
        }
    }
}
