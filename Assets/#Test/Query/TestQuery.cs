using SimpleFrame;

namespace SimpleFrameTest
{
    public class TestQuery : IQuery<int>
    {
        public int Query()
        {
            return 1;
        }
    }
}