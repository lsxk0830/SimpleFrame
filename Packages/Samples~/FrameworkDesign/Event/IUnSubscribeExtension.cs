using UnityEngine;
namespace Blue
{
    public static class IUnSubscribeExtension
    {
        public static void UnSubScribeWhenGameObjectDestroyed(this  IUnSubscribe unSubscribe,GameObject gameObject) 
        {
            var trigger= gameObject.GetComponent<UnSubscribeOnDestroyTrigger>();
            if (!trigger) 
            {
                trigger = gameObject.AddComponent<UnSubscribeOnDestroyTrigger>();
            }
            trigger.AddUnSubscribe(unSubscribe);
        }
        public static void UnSubScribeWhenGameObjectDisabled(this IUnSubscribe unSubscribe, GameObject gameObject)
        {
            var trigger = gameObject.GetComponent<UnSubscribeOnDisableTrigger>();
            if (!trigger)
            {
                trigger = gameObject.AddComponent<UnSubscribeOnDisableTrigger>();
            }
            trigger.AddUnSubscribe(unSubscribe);
        }

    }
}
