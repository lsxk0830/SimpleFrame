using System.Collections.Generic;
using UnityEngine;
namespace Blue
{
    public class UnSubscribeChainEventOnDestroyTrigger : MonoBehaviour
    {
        private HashSet<IChainEventUnSubscribe> unSubscribes = new HashSet<IChainEventUnSubscribe>();

        public void AddUnSubscribe(IChainEventUnSubscribe unSubscribe)
        {
            unSubscribes.Add(unSubscribe);
        }
        private void OnDestroy()
        {
            foreach (var item in unSubscribes)
            {
                item.UnSubscribeAllEventsOnChain();
            }
            unSubscribes.Clear();
        }
    }
}
