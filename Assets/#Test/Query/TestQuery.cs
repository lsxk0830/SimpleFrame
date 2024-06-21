using SimpleFrame;

namespace SimpleFrameTest
{
    public class TestQuery1 : IQuery<int>
    {
        public int Query()
        {
            int testInt = this.GetService<ITestService>().TestInt;
            return testInt;
        }
    }

    public class TestQuery2 : IQuery<string>
    {
        public string Query()
        {
            string TestBind = this.GetService<ITestService>().TestBind.Value;
            return TestBind;
        }
    }
}