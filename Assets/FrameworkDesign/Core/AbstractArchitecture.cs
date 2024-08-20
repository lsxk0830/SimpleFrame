using System;
using System.Collections.Generic;

namespace Blue
{
    /// <summary>
    /// Architecture抽象类，继承自IArchitecture
    /// </summary>
    public abstract class AbstractArchitecture<T>:IArchitecture where T:AbstractArchitecture<T>,new()
    {
        private static IArchitecture architectureInstance;
        private List<IModel> modelList;
        private List<IService> serviceList;
        private IOCContainer modelContainer = new IOCContainer();
        private IOCContainer serviceContainer=new IOCContainer();
        private IOCContainer utilityContainer = new IOCContainer();
        private ITypeEventSystem eventSystem=new DefaultTypeEventSystem();
        private ICommandHandler commandHandle=new DefaultCommandHandler();
        private IQueryHandler queryHandler = new DefaultQueryHandler();

        /// <summary>
        /// 设置Architecture_Instance
        /// </summary>
        void IArchitecture.SetArchitectureInstance(IArchitecture instance)
        {
            architectureInstance = instance;
            ICanGetModelExtension.SetArchitecture(architectureInstance);
            ICanGetServiceExtension.SetArchitecture(architectureInstance);
            ICanGetUtilityExtension.SetArchitecture(architectureInstance);
            IArchitectureModuleExtension.SetArchitecture(architectureInstance);
            ICanSendCommandExtension.SetCommandHandler(commandHandle);
            ICanTriggerEventExtension.SetEventSystem(eventSystem);
            ICanSubscribeEventExtension.SetEventSystem(eventSystem);
            IChainEventUnSubcribeExtension.SetEventSystem(eventSystem);
            ICanSendQueryExtension.SetQueryHandler(queryHandler);
        }

        /// <summary>
        /// 注册
        /// </summary>
        void IArchitecture.Register()
        {
            modelList = new List<IModel>();
            serviceList = new List<IService>();
            OnInit();
        }

        /// <summary>
        /// 初始化Architecture
        /// </summary>
        void IArchitecture.InitArchitecture()
        {
            InitModels();
            InitServices();
        }

        /// <summary>
        /// 完成初始化Architecture
        /// </summary>
        void IArchitecture.FinishInit()
        {
            modelList.Clear();
            serviceList.Clear();
            modelList = null;
            serviceList = null;
        }

        /// <summary>
        /// 初始化 Model 层
        /// </summary>
        private void InitModels()
        {
            foreach (var model in modelList)
            {
                model.OnInit();
            }
            /*modelList.Clear();
            modelList = null;*/
        }

        /// <summary>
        /// 初始化 Services 层
        /// </summary>
        private void InitServices()
        {
            foreach (var service in serviceList)
            {
                service.OnInit();
            }
            /*serviceList.Clear();
            serviceList = null;*/
        }

        protected abstract void OnInit();

        /// <summary>
        /// 设置事件系统
        /// </summary>
        protected virtual void SetTypeEventSystem(ITypeEventSystem typeEventSystem)
        {
            eventSystem = typeEventSystem;
        }

        /// <summary>
        /// 获取 Model
        /// </summary>
        public K GetModel<K>() where K:IModel
        {
            return modelContainer.Get<K>();
        }

        /// <summary>
        /// 获取 Service
        /// </summary>
        public K GetService<K>() where K : IService
        {
            return serviceContainer.Get<K>();
        }

        /// <summary>
        /// 获取 Utility
        /// </summary>
        public K GetUtility<K>() where K : IUtility
        {
            return utilityContainer.Get<K>();
        }

        /// <summary>
        /// 获取 Model
        /// </summary>
        public object GetModel(Type type)
        {
            return modelContainer.Get(type);
        }

        /// <summary>
        /// 获取 Service
        /// </summary>
        public object GetService(Type type)
        {
            return serviceContainer.Get(type);
        }

        /// <summary>
        /// 获取 Utility
        /// </summary>
        public object GetUtility(Type type)
        {
            return utilityContainer.Get(type);
        }

        /// <summary>
        /// 注册 Model
        /// </summary>
        public void RegisterModel<K>() where K :  IModel, new()
        {
            RegisterModel(new K());
        }

        /// <summary>
        /// 注册 Service
        /// </summary>
        public void RegisterService<K>() where K : IService, new()
        {
            RegisterService(new K());
        }

        /// <summary>
        /// 注册 Utility
        /// </summary>
        public void RegisterUtility<K>() where K : IUtility, new()
        {
            RegisterUtility(new K());
        }

        /// <summary>
        /// 注册 Model
        /// </summary>
        public void RegisterModel<K>(K modelInstance) where K : IModel
        {
            modelContainer.Register(modelInstance);
            modelList.Add(modelInstance);
        }

        /// <summary>
        /// 注册 Service
        /// </summary>
        public void RegisterService<K>(K serviceInstance) where K : IService
        {
            serviceContainer.Register(serviceInstance);
            serviceList.Add(serviceInstance);
        }

        /// <summary>
        /// 注册 Utility
        /// </summary>
        public void RegisterUtility<K>(K utilityInstance) where K : IUtility
        {
            utilityContainer.Register(utilityInstance);
        }
    }
}
