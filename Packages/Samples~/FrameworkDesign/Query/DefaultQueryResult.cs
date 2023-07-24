using System;

namespace Blue
{
    /// <summary>
    /// 默认查询结果类,继承IQueryResult<R>
    /// ① 添加查询成功事件
    /// ② 添加查询失败事件
    /// ③ 触发成功事件
    /// ④ 触发失败事件
    /// </summary>
    public class DefaultQueryResult<R> : IQueryResult<R>
    {
        private Action<R> mOnQuerySucceed;
        private Action mOnQueryFailed;

        /// <summary>
        /// 添加查询成功事件
        /// </summary>
        public void OnQuerySucceed(Action<R> onQuerySucceed)
        {
            mOnQuerySucceed += onQuerySucceed;
        }

        /// <summary>
        /// 添加查询失败事件
        /// </summary>
        public void OnQueryFailed(Action onQueryFailed)
        {
            mOnQueryFailed += onQueryFailed;
        }

        public void TriggerSuccess(R result)
        {
            mOnQuerySucceed?.Invoke(result);
        }
        public void TriggerFailed()
        {
            mOnQueryFailed?.Invoke();
        }
    }
}
