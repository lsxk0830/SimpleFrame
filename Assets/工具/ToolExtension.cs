using UnityEngine;

public static class ToolExtension
{
    public static bool IsNull(this GameObject obj)
    {
        return ReferenceEquals(obj, null);
    }

    public static bool InstanceIsNull(this object obj)
    {
        return ReferenceEquals(obj, null);
    }
}