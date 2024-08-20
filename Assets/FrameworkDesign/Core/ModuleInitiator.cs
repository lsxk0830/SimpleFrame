using System;
using System.Collections.Generic;

namespace Blue
{
    public class ModuleInitiator:IDisposable
    {
        private Type moduleInterfaceType;
        private List<Type> moduleList;
        public ModuleInitiator() 
        {
            moduleInterfaceType = typeof(IArchitectureModule);
            moduleList = new List<Type>();
        }
        public void PrepairModuleData(Type type) 
        {
            if (IsModule(type)) 
            {
                moduleList.Add(type);
            }
        }
        public void InitModule() 
        {
            int moduleCount = moduleList.Count;
            if (moduleCount == 0) 
            {
                return;
            }
            for (int i = 0; i < moduleCount; i++)
            {
                IArchitectureModule module =(IArchitectureModule)Activator.CreateInstance(moduleList[i]);
                module.OnInit();
            }
        }

        public void Dispose() 
        {
            moduleInterfaceType = null;
            moduleList = null;
        }
        private bool IsModule(Type type)
        {
            if (type.IsInterface)
            {
                return false;
            }
            if (type.IsAbstract)
            {
                return false;
            }
            return moduleInterfaceType.IsAssignableFrom(type);
        }
    }
}
