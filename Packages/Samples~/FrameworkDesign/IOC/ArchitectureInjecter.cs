using System.Diagnostics;
using System.Linq.Expressions;
using System;

namespace Blue
{
    /// <summary>
    /// Architecture注入类
    /// 主要负责四层 Controller、Model、Service、Utility的分别注入
    /// </summary>
    public class ArchitectureInjecter:IInjector
    {
        private IArchitectureComponentInjector controllerInjector;
        private IArchitectureComponentInjector serviceInjector;
        private IArchitectureComponentInjector modelInjector;
        private IArchitectureComponentInjector utilityInjector;

        /// <summary>
        /// ArchitectureInjecter初始化
        /// 主要初始化四层 Controller、Model、Service、Utility的注入
        /// </summary>
        public ArchitectureInjecter()
        {
            controllerInjector = new ControllerInjector();
            serviceInjector = new ServiceInjector();
            modelInjector = new ModelInjector();
            utilityInjector = new UtilityInjector();
        }

        public void PrepairInjectionData(Type processtype)
        {
            PrepairInjectionDataImpl(processtype);
        }

        /// <summary>
        /// 根据传入的类型注入到指定的层注入
        /// </summary>
        /// <param name="baseType">传入的类型</param>
        private void PrepairInjectionDataImpl(Type baseType)
        {
            if (!TypeChecker.Instance.IsCanInject(baseType))
            {
                return;
            }

            if (TypeChecker.Instance.IsController(baseType))
            {
                PrepairControllerInjectionData(baseType);
            }
            if (TypeChecker.Instance.IsService(baseType))
            {
                PrepairServiceInjectionData(baseType);
            }
            if (TypeChecker.Instance.IsModel(baseType))
            {
                PrepairModelInjectionData(baseType);
            }
            if (TypeChecker.Instance.IsUtility(baseType))
            {
                PrepairUtilityInjectionData(baseType);
            }
        }

        /// <summary>
        /// 设置 Architecture
        /// Controller、Model、Service、Utility注入分别设置architecture
        /// </summary>
        public void Inject(IArchitecture architecture)
        {
            InjectImpl(architecture);
        }

        /// <summary>
        /// 执行与释放或重置非托管资源相关的应用程序定义的任务
        /// </summary>
        public void Dispose()
        {
            controllerInjector.Dispose();
            serviceInjector.Dispose();
            modelInjector.Dispose();
            utilityInjector.Dispose();
            controllerInjector = null;
            serviceInjector = null;
            modelInjector = null;
            utilityInjector = null;
        }

        /// <summary>
        /// Controller、Model、Service、Utility注入设置architecture
        /// </summary>
        /// <param name="architecture"></param>
        private void InjectImpl(IArchitecture architecture)
        {
            controllerInjector.Inject(architecture);
            serviceInjector.Inject(architecture);
            modelInjector.Inject(architecture);
            utilityInjector.Inject(architecture);
        }

        /// <summary>
        /// 预注入Controller数据
        /// </summary>
        private void PrepairControllerInjectionData(Type baseType)
        {
            controllerInjector.PrepairInjectionData(baseType);
        }

        /// <summary>
        /// 预注入Model数据
        /// </summary>
        private void PrepairModelInjectionData(Type baseType)
        {
            modelInjector.PrepairInjectionData(baseType);
        }

        /// <summary>
        /// 预注入Service数据
        /// </summary>
        private void PrepairServiceInjectionData(Type baseType)
        {
            serviceInjector.PrepairInjectionData(baseType);
        }

        /// <summary>
        /// 预注入Utility数据
        /// </summary>
        private void PrepairUtilityInjectionData(Type baseType)
        {
            utilityInjector.PrepairInjectionData(baseType);
        }
    }
}
