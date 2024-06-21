using SimpleFrame;

namespace SimpleFrameTest
{
    public class TestService : ITestService
    {
        public int TestInt { get; private set; }
        public BindableProperty<string> TestBind { get; private set; } = new BindableProperty<string>();

        public void Init()
        {
            TestInt = 321;
            TestBind.Value = "456";
        }
    }
}