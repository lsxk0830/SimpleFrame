using System.Diagnostics;
using System;
using System.Reflection;
using UnityEngine.Scripting;

namespace Blue
{
    /// <summary>
    /// 标记该类型时，也将标记从该类型派生的所有类型
    /// </summary>
    [RequireDerived]
    public class Singleton<T> where T:Singleton<T>
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Init();
                }
                return _instance;
            }
        }
        private static T Init()
        {
            Type type = typeof(T);
            // type.GetConstructors: 获取构造函数
            ConstructorInfo[] ctorArr=type.GetConstructors(BindingFlags.Instance|BindingFlags.NonPublic);
            if (ctorArr.Length==0)
            {
                UnityEngine.Debug.LogError($"在“{type.FullName}”中没有找到公共构造函数");
                throw new Exception("Non Public Contstrucor Not Found In "+type.FullName);
            }
            ConstructorInfo ctor=Array.Find(ctorArr,c=> c.GetParameters().Length==0);
            if (ctor == null)
            {
                throw new Exception("Non Public Contstrucor Not Found In " + type.FullName);
            }
            _instance=ctor.Invoke(null) as T;
            return _instance;
        }
    }
}
