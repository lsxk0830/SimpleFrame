using System.Collections.Generic;
using UnityEngine;
namespace Blue
{
    public class UnSubscribeOnDisableTrigger : MonoBehaviour
    {
        private HashSet<IUnSubscribe> unSubscribes = new HashSet<IUnSubscribe>();

        public void AddUnSubscribe(IUnSubscribe unSubscribe) 
        {
            unSubscribes.Add(unSubscribe);
        }
        private void OnDisable()
        {
            foreach (var item in unSubscribes)
            {
                item.UnSubscribe();
            }
            unSubscribes.Clear();
        }
    }
}
