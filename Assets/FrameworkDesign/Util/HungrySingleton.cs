using System;
using System.Reflection;

namespace Blue
{
    public class HungrySingleton<T> where T : HungrySingleton<T>
    {
        private static T _instance = init();

        public static T Instance
        {
            get => _instance;
        }

        private static T init()
        {
            Type type = typeof(T);
            ConstructorInfo[] ctorArr = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
            if (ctorArr.Length == 0)
            {
                throw new Exception("Non Public Contstrucor Not Found In " + type.FullName);
            }
            ConstructorInfo ctor = Array.Find(ctorArr, c => c.GetParameters().Length == 0);
            if (ctor == null)
            {
                throw new Exception("Non Public Contstrucor Not Found In " + type.FullName);
            }
            _instance = ctor.Invoke(null) as T;
            return _instance;
        }
    }
}
