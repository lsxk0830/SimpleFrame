using SimpleFrame;
using UnityEngine;

namespace SimpleFrameTest
{
    public class QueryTest : MonoBehaviour, ICanQuery
    {
        void Start()
        {
            int testInt = this.DoQuery<TestQuery1, int>();
            string testBind = this.DoQuery<TestQuery2, string>();
            Debug.Log($"测试Int:{testInt},TestBind:{testBind}");
        }
    }
}