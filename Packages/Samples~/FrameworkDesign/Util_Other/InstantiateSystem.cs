using System.Collections.Generic;
using UnityEngine;

namespace Blue
{
    public class InstantiateSystem : SingletonMonobehaviour<InstantiateSystem>
    {
        [SerializeField] private BlueObject blueObject;
        public BlueObject BlueObject => blueObject;

        public Dictionary<string, GameObject> objDic = new Dictionary<string, GameObject>();

        public void InstantiatePrefab(GameObject prefab, Transform parent=null)
        {
            Instantiate(prefab,parent);
        }
    }
}