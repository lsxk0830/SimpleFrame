using UnityEngine;

[CreateAssetMenu(fileName ="PrefabsAsset",menuName ="ScriptableObject/BluePrefabs",order = 1 )]
public class BlueObject : ScriptableObject
{
    //可以包含更多的数据，信息
    public GameObject WarningPopUp;
    public string NetErrorText;
    public string SuccessText;
}