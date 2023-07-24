using System;
namespace Blue
{
    /// <summary>
    /// 异步查询的接口
    /// </summary>
    public interface IQueryAsync : ICanGetService, ICanGetModel, ICanGetUtility, ICanSubscribeEvent
    {
        /// <summary>
        /// 异步查询方法
        /// </summary>
        void DoQueryAsync<R>(Action<R> onQueryComleted);
    }
}
