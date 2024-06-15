using System.Collections.Generic;

namespace SimpleFrame
{
    public abstract class AbstractArchitecture<T> : IArchitecture where T : AbstractArchitecture<T>, new()
    {
        private ArchitectureIOC mIOCContainer = new ArchitectureIOC();
        private HashSet<IService> mServiceList = new HashSet<IService>();
        private HashSet<IModel> mModelList = new HashSet<IModel>();
        public void Init()
        {
            CanGetModelExtension.SetArchitecture(this);
            CanGetServiceExtension.SetArchitecture(this);
            CanGetUtilityExtension.SetArchitecture(this);
            CanSendCommandExtension.SetArchitecture(this);
            CanDoQueryExtension.SetArchitecture(this);

            OnInit();

            foreach (var model in mModelList)
            {
                model.Init();
            }
            foreach (var service in mServiceList)
            {
                service.Init();
            }
        }

        protected abstract void OnInit();

        void IArchitecture.RegisterModel<TModel>(TModel instance)
        {
            mModelList.Add(instance);
            mIOCContainer.Push<TModel>(instance);
        }

        void IArchitecture.RegisterService<TService>(TService instance)
        {
            mServiceList.Add(instance);
            mIOCContainer.Push<TService>(instance);
        }

        void IArchitecture.RegisterUtility<TUtility>(TUtility instance)
        {
            mIOCContainer.Push<TUtility>(instance);
        }

        TModel IArchitecture.GetModel<TModel>()
        {
            return mIOCContainer.Pull<TModel>();
        }

        TService IArchitecture.GetService<TService>()
        {
            return mIOCContainer.Pull<TService>();
        }

        TUtility IArchitecture.GetUtility<TUtility>()
        {
            return mIOCContainer.Pull<TUtility>();
        }

        void IArchitecture.SendCommand<TCommand>()
        {
            TCommand command = this.GetObjInstance<TCommand>();
            command.Execute();
            this.PushPool(command);
        }

        void IArchitecture.SendCommand<TCommand>(TCommand command)
        {
            command.Execute();
            this.PushPool(command);
        }

        Result IArchitecture.DoQuery<TQuery, Result>()
        {
            IQuery<Result> query = this.GetObjInstance<TQuery>();
            Result result = query.Query();
            this.PushPool(query);
            return result;
        }

        Result IArchitecture.DoQuery<TQuery, Result>(IQuery<Result> query)
        {
            Result result = query.Query();
            this.PushPool(query);
            return result;
        }
    }
}