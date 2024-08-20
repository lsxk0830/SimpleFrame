namespace Blue
{

    public static class ICanSendQueryExtension
    {
        private static IQueryHandler _queryHandler;

        public static void SetQueryHandler(IQueryHandler queryHandler)
        {
            _queryHandler = queryHandler;
        }

        public static R SendQuery<Q,R>(this ICanSendQuery self) where Q : IQuery<R>,new()
        {
            return _queryHandler.DoQuery<Q,R>();
        }
        public static R SendQuery<R>(this ICanSendQuery self, IQuery<R> queryInstance)
        {
            return _queryHandler.DoQuery<R>(queryInstance);
        }
        public static IQueryResult<R> SendQueryAsync<Q, R>(this ICanSendQuery self) where Q : IQuery<R>,new()
        {
            return _queryHandler.DoQueryAsync<Q, R>();
        }
        public static IQueryResult<R> SendQueryAsync<R>(this ICanSendQuery self, IQuery<R> queryInstance)
        {
            return _queryHandler.DoQueryAsync<R>(queryInstance);
        }
    }
}
