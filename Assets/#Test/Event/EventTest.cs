using UnityEngine;

public class EventTest : AbstractController
{
    private TestClassEvent TestClassEvent;
    private TestStructEvent TestStructEvent;

    private void Start()
    {
        this.RegisterEvent<TestClassEvent>(TestClassEventListener); // 事件监听
        this.RegisterEvent<TestStructEvent>(TestStructEventListener); // 事件监听
    }
    private void TestClassEventListener(TestClassEvent e)
    {
        Debug.Log($"Class事件:{e.Name}");
    }
    private void TestStructEventListener(TestStructEvent e)
    {
        Debug.Log($"Struct事件:{e.ID}");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            this.TriggerEvent<TestClassEvent>(); // 触发事件
            this.TriggerEvent<TestStructEvent>(); // 触发事件
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            TestClassEvent = this.GetObjInstance<TestClassEvent>(); // 推荐从对象池中获取对象
            TestClassEvent.Name = "W";

            TestStructEvent = this.GetObjInstance<TestStructEvent>();
            TestStructEvent.ID = 1;
            this.TriggerEvent(TestClassEvent); // 触发事件
            this.TriggerEvent(TestStructEvent); // 触发事件
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            TestClassEvent = this.GetObjInstance<TestClassEvent>();
            TestClassEvent.Name = "E";

            TestStructEvent = this.GetObjInstance<TestStructEvent>();
            TestStructEvent.ID = 2;
            this.TriggerEvent(TestClassEvent);
            this.TriggerEvent(TestStructEvent);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            this.TriggerEvent<TestClassEvent>(); // 触发事件
            this.TriggerEvent<TestStructEvent>(); // 触发事件
        }
    }
}