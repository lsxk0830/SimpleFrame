using System;
namespace Blue
{
    /// <summary>
    /// 查询句柄的接口
    /// 同步+异步查询
    /// </summary>
    public interface IQueryHandler
    {
        /// <summary>
        /// 同步查询
        /// </summary>
        R DoQuery<Q,R>() where Q:IQuery<R>,new();

        /// <summary>
        /// 同步查询
        /// </summary>
        R DoQuery<R>(IQuery<R> query);

        /// <summary>
        /// 异步查询
        /// </summary>
        IQueryResult<R> DoQueryAsync<R>(IQuery<R> query);

        /// <summary>
        /// 异步查询
        /// </summary>
        IQueryResult<R> DoQueryAsync<Q,R>() where Q:IQuery<R>,new();
    }
}
