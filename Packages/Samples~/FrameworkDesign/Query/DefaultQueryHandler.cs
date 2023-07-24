using System.Threading.Tasks;

namespace Blue
{
    /// <summary>
    /// 默认查询句柄类
    /// </summary>
    public class DefaultQueryHandler : IQueryHandler
    {
        /// <summary>
        /// 同步查询
        /// </summary>
        public R DoQuery<Q, R>() where Q : IQuery<R>, new()
        {
            return DoQuery(new Q());
        }

        /// <summary>
        /// 同步查询
        /// </summary>
        public R DoQuery<R>(IQuery<R> query)
        {
            return query.DoQuery();
        }

        /// <summary>
        /// 异步查询
        /// </summary>
        public IQueryResult<R> DoQueryAsync<R>(IQuery<R> query)
        {
            DefaultQueryResult<R> queryResult = new DefaultQueryResult<R>();
            var queryTask = Task.Run(() => {
                return query.DoQuery();
            });
            var awaiter = queryTask.GetAwaiter();
            awaiter.OnCompleted(() => {
                /*
                Task.IsFaulted:
                获取 Task 是否由于未经处理异常的原因而完成,如果任务引发了未经处理的异常，则为 true；否则为 false
                Task.IsCanceled
                获取此 Task 实例是否由于被取消的原因而已完成执行,如果任务由于被取消而完成，则为 true；否则为 false
                */
                if (queryTask.IsFaulted | queryTask.IsCanceled)
                {
                    queryResult.TriggerFailed();
                }
                else
                {
                    queryResult.TriggerSuccess(awaiter.GetResult());
                }
            });
            return queryResult;
        }

        /// <summary>
        /// 异步查询
        /// </summary>
        public IQueryResult<R> DoQueryAsync<Q, R>() where Q : IQuery<R>, new()
        {
            return DoQueryAsync(new Q());
        }
    }
}
