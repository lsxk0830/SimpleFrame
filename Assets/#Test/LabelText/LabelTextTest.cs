using UnityEngine;

public class LabelTextTest : MonoBehaviour
{
    [LabelText("这是一个物体")]
    public GameObject go;

    [LabelText("这是一个数字")]
    public int Number;

#if UNITY_EDITOR
    [LabelText("这是一个名字")]
#endif
    public int Name;
}
