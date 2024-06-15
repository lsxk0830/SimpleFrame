namespace SimpleFrameTest
{
    public class TestUtility : ITestUtility
    {
        int ITestUtility.TestGetStringLength(string str)
        {
            return str.Length;
        }
    }
}