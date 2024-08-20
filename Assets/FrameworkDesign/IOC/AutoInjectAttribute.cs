using System;

namespace Blue
{
    /// <summary>
    /// 自动注入---可以对字段应用属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class AutoInjectAttribute : Attribute
    {
        private Type type;
        private InjectScope scope;
        public AutoInjectAttribute()
        {
            scope = InjectScope.Singleton;
        }
        public AutoInjectAttribute(Type injectType):base()
        {
            type = injectType;
        }
        public AutoInjectAttribute(Type injectType, InjectScope injectScope)
        {
            type = injectType;
            scope = injectScope;
        }
        public Type GetInjectType()
        {
            return type;
        }
        public InjectScope GetInjectScope()
        {
            return scope;
        }

    }
}
