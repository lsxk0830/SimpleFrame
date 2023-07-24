namespace Blue
{
	public interface IAsyncInvocationHandler<T>
	{
		void TriggerFailed();
		void TriggerSucceed(T result);
	}
}
