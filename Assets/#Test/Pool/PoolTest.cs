using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SimpleFrame;

namespace SimpleFrameTest
{
    public class PoolTest : MonoBehaviour
    {
        public class PoolClassTest
        {
            public int ID = 999;
        }

        private GameObject prefab;
        private PoolClassTest mPoolClassTest;
        private List<GameObject> mGoList = new List<GameObject>();

        private void Start()
        {
            prefab = new GameObject("Prefab");
            prefab.name = prefab.GetInstanceID().ToString();
            mGoList.Add(prefab);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                mPoolClassTest = this.GetObjInstance<PoolClassTest>(); // 从对象池中获取
                Debug.Log($"获取普通C#类_并打印:{mPoolClassTest.ID}");
                mPoolClassTest.ID = 666;

                GameObject go = this.GetGameObject(prefab); // 从对象池中获取
                mGoList.Add(go);
                go.name = go.GetInstanceID().ToString();
                Debug.Log($"获取MonoID_并打印:{go.GetInstanceID()}");
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                this.PushPool(mPoolClassTest); // 放入对象池
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                mPoolClassTest = this.GetObjInstance<PoolClassTest>(); // 从对象池中获取
                Debug.Log($"获取普通C#类_并打印:{mPoolClassTest.ID}"); // 666,上一个对象的值，所以从对象池中获取到数据后应该对其初始化
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                this.PushGameObject(mGoList.First());  // 放入对象池
                mGoList.RemoveAt(0);
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                GameObject go = this.GetGameObject(prefab); // 从对象池中获取,prefab为关闭状态则获取到的对象为关闭状态
                mGoList.Add(go);
                go.name = go.GetInstanceID().ToString();
                Debug.Log($"获取MonoID_并打印:{go.GetInstanceID()}");
            }
        }
    }
}