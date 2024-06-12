using System;

public static class CanEventExtension
{
    private static EventIOC Global = new EventIOC();

    public static void RegisterEvent<T>(this ICanEvent self, Action<T> onEvent) where T : IEvent
    {
        Global.RegisterEvent<T>(onEvent);
    }

    public static void UnRegisterEvent<T>(this ICanEvent self, Action<T> onEvent) where T : IEvent
    {
        Global.UnRegisterEvent<T>(onEvent);
    }

    public static void TriggerEvent<T>(this ICanEvent self, T instance) where T : IEvent
    {
        Global.InvokeEvent<T>(instance);
    }

    public static void TriggerEvent<T>(this ICanEvent self) where T : IEvent, new()
    {
        Global.InvokeEvent<T>();
    }
}