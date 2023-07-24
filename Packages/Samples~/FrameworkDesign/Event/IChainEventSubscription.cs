using System;
using System.Collections.Generic;

namespace Blue
{
	/// <summary>
	/// 订阅链事件的接口
	/// </summary>
	public interface IChainEventSubscription
	{
		int SubscribeCount { get; }
		IChainEventUnSubscribe Subscribe(Type eventType, int actionHashCode, Action onOnSubscribe);
		List<Type> GetEventTypeList();
		bool IsSubscribed(Type eventType);
		int GetActionHashCode(Type eventType);
		bool UnSubscribe(Type eventType);
	}
}
