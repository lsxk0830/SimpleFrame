using System.Collections.Generic;
using UnityEngine;
namespace Blue
{
    public class UnSubscribeOnDestroyTrigger : MonoBehaviour
    {
        private HashSet<IUnSubscribe> unSubscribes = new HashSet<IUnSubscribe>();

        public void AddUnSubscribe(IUnSubscribe unSubscribe) 
        {
            unSubscribes.Add(unSubscribe);
        }
        private void OnDestroy()
        {
            foreach (var item in unSubscribes)
            {
                item.UnSubscribe();
            }
            unSubscribes.Clear();
        }
    }
}
