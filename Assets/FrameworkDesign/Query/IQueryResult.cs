using System;
namespace Blue
{
    /// <summary>
    /// 查询结果的接口
    /// ① 添加查询成功事件
    /// ② 添加查询失败事件
    /// </summary>
    public interface IQueryResult<R>
    {
        /// <summary>
        /// 添加查询成功事件
        /// </summary>
        void OnQuerySucceed(Action<R> onQuerySucceed);

        /// <summary>
        /// 添加查询失败事件
        /// </summary>
        void OnQueryFailed(Action onQueryFailed);
    }
}
