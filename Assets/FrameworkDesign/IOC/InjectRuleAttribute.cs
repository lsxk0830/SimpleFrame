using System;
using System.Collections.Generic;
using System.Linq;

namespace Blue
{
    /// <summary>
    /// 注入规则属性---可以对接口应用属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface)]
    public class InjectRuleAttribute : Attribute
    {
        private List<Type> canInjectList;
        private Type baseInjectType;
        public InjectRuleAttribute(params Type[] canInject)
        {
            baseInjectType = typeof(ICanInject);
            canInjectList = new List<Type>(canInject.Length);
            foreach (var item in canInject)
            {
                if (item.GetInterfaces().Contains(baseInjectType))
                {
                    canInjectList.Add(item);
                }
            }
        }
        public List<Type> GetCanInjectList()
        {
            return canInjectList;
        }
        public bool IfCanInject(Type canInject)
        {
            if (canInjectList.Contains(canInject))
            {
                return true;
            }
            return false;
        }
    }
}
