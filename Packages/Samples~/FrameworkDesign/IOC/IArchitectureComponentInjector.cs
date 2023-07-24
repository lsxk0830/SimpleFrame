using System;
namespace Blue
{
    /// <summary>
    /// 实现Architecture组件注入的接口
    /// </summary>
    public interface IArchitectureComponentInjector:IDisposable
    {
        /// <summary>
        /// 根据类型预注入数据
        /// </summary>
        void PrepairInjectionData(Type baseType);

        /// <summary>
        /// architecture注入
        /// </summary>
        void Inject(IArchitecture architecture);
    }
}
