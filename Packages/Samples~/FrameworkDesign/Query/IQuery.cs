namespace Blue
{
    /// <summary>
    /// 同步查询的接口
    /// </summary>
    public interface IQuery<R>:ICanGetService,ICanGetModel,ICanGetUtility,ICanSubscribeEvent,ICanSendQuery
    {
        /// <summary>
        /// 同步查询方法
        /// </summary>
        R DoQuery();
    }
}
