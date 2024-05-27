public static class DebugLog
{
    public static void Log(string LogStr)
    {
#if UNITY_EDITOR
        UnityEngine.Debug.Log(LogStr);
#endif
    }

    public static void Warning(string LogStr)
    {
#if UNITY_EDITOR
        UnityEngine.Debug.LogWarning(LogStr);
#endif
    }

    public static void Error(string LogStr)
    {
#if UNITY_EDITOR
        UnityEngine.Debug.LogError(LogStr);
#endif
    }
}
