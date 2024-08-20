namespace Blue
{
	/// <summary>
	/// 异步处理接口
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IAsyncInvocationHandler<T>
	{
		/// <summary>
		/// 触发失败
		/// </summary>
		void TriggerFailed();

		/// <summary>
		/// 成功触发
		/// </summary>
		/// <param name="result"></param>
		void TriggerSucceed(T result);
	}
}
