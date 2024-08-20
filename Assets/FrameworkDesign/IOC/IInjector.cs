using System;
namespace Blue
{
    /// <summary>
    /// 实现注入功能的接口
    /// </summary>
    public interface IInjector:IDisposable
    {
        /// <summary>
        /// 根据类型预注入数据
        /// </summary>
        void PrepairInjectionData(Type processType);

        /// <summary>
        /// architectureInstance注入
        /// </summary>
        void Inject(IArchitecture architectureInstance);
    }
}
