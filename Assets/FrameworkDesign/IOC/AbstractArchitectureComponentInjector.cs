using System;
using System.Reflection;
using System.Collections.Generic;

namespace Blue
{
    public abstract class AbstractArchitectureComponentInjector : IArchitectureComponentInjector
    {
        private Type mComponentType;
        private bool canInjectTypeListNotInitiated=true;
        private List<Type> canInjectTypeList;
        private Dictionary<Type, List<InjectInfo>> injectionMap;
        protected IArchitecture architectureInstance;

        public AbstractArchitectureComponentInjector(Type componentType)
        {
            SetComponentType(componentType);
        }
        public void SetArchitecture(IArchitecture architecture)
        {
            if (architectureInstance == null)
            {
                architectureInstance = architecture;
            }
        }
        private void SetComponentType(Type componentType)
        {
            mComponentType = componentType;
        }

        public virtual void PrepairInjectionData(Type baseType)
        {
            if (canInjectTypeListNotInitiated)
            {
                canInjectTypeListNotInitiated = false;
                injectionMap = new Dictionary<Type, List<InjectInfo>>();
                canInjectTypeList = GetCanInjectTypeList(mComponentType);
            }
            FilterInjectInfoList(GetInjectInfo(baseType), mComponentType);
        }
        public virtual void Inject(IArchitecture architecture)
        {
            if (injectionMap == null)
            {
                return;
            }
            SetArchitecture(architecture);
            int injectionCount = injectionMap.Count;
            List<Type> types = new List<Type>(injectionCount);
            types.AddRange(injectionMap.Keys);
            if (injectionCount > 0)
            {
                for (int i = 0; i < injectionCount; i++)
                {
                    Type baseType = types[i];
                    object injectObject = GetInjectObject(baseType);
                    if (injectObject == null)
                    {
                        continue;
                        //throw new Exception("Can not find instance of "+baseType.FullName+" ,please make sure you have registed it.");
                    }
                    if (!injectObject.GetType().Equals(baseType))
                    {
                        continue;//Not the registed Type
                    }
                    List<InjectInfo> typeInjectInfoList = injectionMap[baseType];
                    foreach (InjectInfo injectInfo in typeInjectInfoList)
                    {
                        if (injectInfo.InjectScope == InjectScope.Prototype)
                        {
                            InjectPrototype(injectObject, injectInfo.InjectField, injectInfo.InjectType);
                        }
                        else
                        {
                            object injectInstance = GetInjectInstance(injectInfo.InjectType);
                            InjectSingleton(injectObject, injectInfo.InjectField, injectInstance);
                        }
                    }
                }
            }
        }
        public void Dispose()
        {
            if (canInjectTypeList != null)
            {
                canInjectTypeList.Clear();
                canInjectTypeList = null;
            }
            if (injectionMap != null)
            {
                injectionMap.Clear();
                injectionMap = null;
            }
            mComponentType = null;
            architectureInstance = null;
        }
        protected virtual object GetInjectObject(Type baseType)
        {
            return null;
        }
        protected virtual object GetInjectInstance(Type injectType)
        {
            if (TypeChecker.Instance.IsService(injectType))
            {
                return architectureInstance.GetService(injectType);
            }
            if (TypeChecker.Instance.IsModel(injectType))
            {
                return architectureInstance.GetModel(injectType);
            }
            if (TypeChecker.Instance.IsUtility(injectType))
            {
                return architectureInstance.GetUtility(injectType);
            }
            throw new Exception(injectType.FullName+" is not a can inject type!");
        }
        protected Dictionary<Type, List<InjectInfo>> GetInjectionMap()
        {
            return injectionMap;
        }
        protected void InjectSingleton(object injectObject, FieldInfo fieldInfo, object injectInstance)
        {
            fieldInfo.SetValue(injectObject, injectInstance);
        }
        protected void InjectPrototype(object injectObject, FieldInfo fieldInfo, Type injectType)
        {
            fieldInfo.SetValue(injectObject, Activator.CreateInstance(injectType));
        }

        private List<Type> GetCanInjectTypeList(Type baseType)
        {
            List<Type> canInjectTypeList = new List<Type>();
            Type[] interfaceArr = baseType.GetInterfaces();
            for (int i = 0; i < interfaceArr.Length; i++)
            {
                Type interfaceType = interfaceArr[i];
                InjectRuleAttribute attribute = null;
                if ((attribute = interfaceType.GetCustomAttribute<InjectRuleAttribute>()) != null)
                {
                    canInjectTypeList.AddRange(attribute.GetCanInjectList());
                }
            }
            return canInjectTypeList;
        }
        private List<InjectInfo> GetInjectInfo(Type tmpType)
        {
            List<InjectInfo> injectInfoList = new List<InjectInfo>();
            FieldInfo[] tmpFieldArr = tmpType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            int length = 0;
            if (tmpFieldArr != null)
            {
                length = tmpFieldArr.Length;
            }
            for (int i = 0; i < length; i++)
            {
                FieldInfo tmpField = tmpFieldArr[i];
                AutoInjectAttribute autoInjectAttribute = tmpField.GetCustomAttribute<AutoInjectAttribute>();
                if (autoInjectAttribute != null)
                {
                    InjectInfo injectInfo = new InjectInfo()
                    {
                        BaseType = tmpType,
                        InjectField = tmpField,
                        InjectScope = autoInjectAttribute.GetInjectScope()
                    };
                    if (autoInjectAttribute.GetInjectType() != null)
                    {
                        if (CheckType(tmpField.FieldType, autoInjectAttribute.GetInjectType()))
                        {
                            injectInfo.InjectType = autoInjectAttribute.GetInjectType();
                        }
                        else
                        {
                            throw new Exception("Can not finish the injection of:" + tmpType.FullName + ": property: " + tmpField.Name + ", please check the injectType!");
                        }
                    }
                    else
                    {
                        injectInfo.InjectType = tmpField.FieldType;
                    }
                    injectInfoList.Add(injectInfo);
                }
            }
            return injectInfoList;
        }
        //if the injectinfo not match the inject rule,than remove it from the injectInfoList
        //for example,you can not  inject a IService instance in a IMolde implementation class 
        private void FilterInjectInfoList(List<InjectInfo> injectInfoList, Type type)
        {
            int count = injectInfoList.Count;
            for (int i = 0; i < count; i++)
            {
                InjectInfo injectInfo = injectInfoList[i];
                if (CheckIfCanInject(canInjectTypeList, injectInfo.InjectType))
                {
                    if (injectionMap.ContainsKey(injectInfo.BaseType))
                    {
                        injectionMap[injectInfo.BaseType].Add(injectInfo);
                    }
                    else
                    {
                        List<InjectInfo> typeInjectInfoList = new List<InjectInfo>();
                        typeInjectInfoList.Add(injectInfo);
                        injectionMap.Add(injectInfo.BaseType, typeInjectInfoList);
                    }
                }
            }
        }
        //check if the fieldType and injectType is mismatch;
        private bool CheckType(Type fieldType, Type injectType)
        {
            if (injectType.Equals(fieldType))
            {
                return true;
            }
            if (fieldType.IsInterface)
            {
                return fieldType.IsAssignableFrom(injectType);
            }
            return injectType.IsSubclassOf(fieldType);
        }
        private bool CheckIfCanInject(List<Type> canInjectTypeList, Type injectType)
        {
            bool result = false;
            foreach (var canInjectType in canInjectTypeList)
            {
                if (canInjectType.IsAssignableFrom(injectType))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
}
