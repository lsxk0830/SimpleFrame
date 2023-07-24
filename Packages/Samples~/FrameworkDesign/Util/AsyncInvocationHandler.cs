using System;

namespace Blue
{
	public class AsyncInvocationHandler<T>:IAsyncInvocationHandler<T>
	{
		private Action _onFailed;
		private Action<T> _onSucceed;
		//call back for process success
		public void OnSucceed(Action<T> onSucceed)
		{
			_onSucceed = onSucceed;
		}
		//call back for process failed
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
