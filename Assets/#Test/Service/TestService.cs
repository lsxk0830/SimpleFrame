using SimpleFrame;

namespace SimpleFrameTest
{
    public class TestService : ITestService
    {
        public int TestInt { get; private set; }
        public BindableProperty<string> TestBind { get; private set; } = new BindableProperty<string>();

        [Inject] public ITestModel testModel;

        public void Init()
        {
            TestInt = 321;
            TestBind.Value = "456";
        }

        public void TestInject()
        {
            this.Log($"注入式ID:{testModel.GetID(351.ToString())}");
        }
    }
}