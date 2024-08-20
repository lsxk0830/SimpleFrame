using System;

namespace Blue
{
	/// <summary>
	/// 异步处理
	/// </summary>
	public class AsyncInvocationHandler<T>:IAsyncInvocationHandler<T>
	{
		private Action _onFailed;
		private Action<T> _onSucceed;
		public void OnSucceed(Action<T> onSucceed)
		{
			_onSucceed = onSucceed;
		}
		public void OnFailed(Action onFailed)
		{
			_onFailed = onFailed;
		}
		void IAsyncInvocationHandler<T>.TriggerFailed()
		{
			_onFailed?.Invoke();
		}
		void IAsyncInvocationHandler<T>.TriggerSucceed(T result)
		{
			_onSucceed?.Invoke(result);
		}
	}
}
