using SimpleFrame;

namespace SimpleFrameTest
{
    public class TestClassEvent : IEvent
    {
        public string Name;
    }

    public struct TestStructEvent : IEvent
    {
        public int ID;
    }
}