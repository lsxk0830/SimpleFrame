using SimpleFrame;

namespace SimpleFrameTest
{
    public interface ITestService : IService
    {
        public int TestInt { get; }
        public BindableProperty<string> TestBind { get; }
        void TestInject();
    }
}