using SimpleFrame;
using UnityEngine;

namespace SimpleFrameTest
{
    public class Test : MonoBehaviour, IController
    {
        void Start()
        {
            this.SendCommand<TestCommand>();
            this.SendCommand<TestCommand>();
            this.SendCommand<TestCommand>();
            this.SendCommand<TestCommand>();
        }
    }
}