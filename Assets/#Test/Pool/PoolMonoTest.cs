using UnityEngine;

public class PoolMonoTest : MonoBehaviour, IPool
{
    public int ID = 0;

    public void Init()
    {
        ID = 99; // 初始化ID
    }

    public void Reset()
    {
        ID = -1; // 重置ID
    }
}