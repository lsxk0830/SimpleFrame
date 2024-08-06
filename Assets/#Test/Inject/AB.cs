using SimpleFrame;
using UnityEngine;

namespace SimpleFrameTest
{
    public class InjectA : AbsatractInject
    {
        [Inject] internal InjectB b;

        [Inject] private InjectC c { get; set; }

        public void Test()
        {
            b.Test();
            c.Test();
        }
    }

    public class InjectB
    {
        public void Test()
        {
            Debug.Log("B");
        }
    }

    public class InjectC : AbsatractInject
    {
        public void Test()
        {
            Debug.Log("C");
        }
    }
}