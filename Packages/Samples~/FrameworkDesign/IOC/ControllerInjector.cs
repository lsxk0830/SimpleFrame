using System;
using System.Collections.Generic;
using UnityEngine;
namespace Blue
{

    public class ControllerInjector : AbstractArchitectureComponentInjector
    {
        public ControllerInjector():base(typeof(IController))
        {
        }
        private  object[] GetInjectObjects(Type baseType)
        {
            return GameObject.FindObjectsOfType(baseType, true);
        }
        public override void Inject(IArchitecture architecture)
        {
            SetArchitecture(architecture);
            Dictionary<Type, List<InjectInfo>> injectionMap = GetInjectionMap();
            if (injectionMap == null)
            {
                return;
            }
            int injectionCount = injectionMap.Count;
            var types = injectionMap.Keys;
            if (injectionCount > 0)
            {
                foreach (Type baseType in types)
                {
                    object[] injectObjects = GetInjectObjects(baseType);
                    List<InjectInfo> typeInjectInfoList = injectionMap[baseType];
                    foreach (InjectInfo injectInfo in typeInjectInfoList)
                    {
                        if (injectInfo.InjectScope == InjectScope.Prototype)
                        {
                            for (int i = 0; i < injectObjects.Length; i++)
                            {
                                InjectPrototype(injectObjects[i], injectInfo.InjectField, injectInfo.InjectType);
                            }
                        }
                        else
                        {
                            object injectInstance = GetInjectInstance(injectInfo.InjectType);
                            for (int i = 0; i < injectObjects.Length; i++)
                            {
                                InjectSingleton(injectObjects[i], injectInfo.InjectField, injectInstance);
                            }
                        }
                    }
                }
            }
        }
    }
}
