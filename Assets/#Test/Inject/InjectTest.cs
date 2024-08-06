using SimpleFrame;
using UnityEngine;

namespace SimpleFrameTest
{
    public class InjectTest : MonoBehaviour, ICanGetService
    {
        [Inject] InjectA injectA;
        void Start()
        {
            DIContainer.Register(new InjectB());
            DIContainer.InjectDependencies(this);
            injectA.Test();

            this.GetService<ITestService>().TestInject();
        }
    }
}