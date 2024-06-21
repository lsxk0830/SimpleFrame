#### 个人理解【仅供参考】

Command、Query主要用于多人协作开发，为对方提供接口时使用。在自己的模块内开发，直接获取或者使用事件即可。

若每一个调用或获取都进行Command、Query,随着项目的持续开发，项目会越来越笨重。

简洁且易开发才是项目最基本的

#### Architecture【必须】

项目全局统一设置一个T类，此类继承AbstractArchitecture<T>。此类用于注册Model、Service、Utility

```c#
public class TestArchitecture : AbstractArchitecture<TestArchitecture>
{
    protected override void OnInit()
    {
        RegisterModel();
        RegisterService();
        RegisterUtility();
    }

    void RegisterModel()
    {
        RegisterModel<ITestModel>(new TestModel());
    }

    void RegisterService()
    {
        RegisterService<ITestService>(new TestService());
    }
    void RegisterUtility()
    {
        RegisterUtility<ITestUtility>(new TestUtility());
    }
}
```



#### 项目设置【必看】

项目分为四个层级，Controller层、Model层、Service层、Utility层
Controller层为表现层，Model层+Service层为底层，Utility层为工具层。工具层全局都可以调用【默认实现了层级接口或层级之间沟通接口】。

表现层【Controller层】与底层【Model层+Service层】之间的交互为：

变现层--->底层:使用Command

底层--->变现层:使用事件



#### 必看规则

##### 层级接口

###### IController

```C#
/// <summary>
/// Controller层默认为显示层，即继承自MonoBehaviour的可视化层。
/// 当然，根据项目的特殊性，有些项目场景中无脚本的挂载。
/// 所以，操控Unity场景面板的脚本都可以默认为可视化层，即挂载IController接口
/// </summary>
public interface IController : ICanEvent, ICanCommand, ICanQuery, ICanGetUtility
{
}
```

###### IModel

```C#
/// <summary>
/// Model层接口，Model层脚本都必须继承 IModel 接口
/// </summary>
public interface IModel : ICanGetModel, ICanGetService, ICanGetUtility,
                          ICanEvent
{
    void Init();
}
```

###### IService

```C#
/// <summary>
/// Service层接口，Service层脚本都必须继承 IService 接口
/// </summary>
public interface IService : ICanGetModel, ICanGetService, ICanGetUtility,
                            ICanEvent
{
    void Init();
}

```

###### IUtility

```C#
/// <summary>
/// Utility层接口，Utility层脚本都必须继承 IUtility 接口
/// </summary>
public interface IUtility : ICanGetModel, ICanGetService, ICanGetUtility
{

}
```

##### 层级之间沟通接口

###### ICommand

```C#
/// <summary>
/// 命令接口，所有命令都必须继承 ICommand 接口
/// </summary>
public interface ICommand : ICanGetModel, ICanGetService, ICanGetUtility,
                            ICanEvent, ICanCommand
{
    void Execute();
}
```

###### IQuery

```C#
/// <summary>
/// 查询接口，所有查询都必须继承 IQuery<T> 接口
/// </summary>
/// <typeparam name="T">查询的返回值类型</typeparam>
public interface IQuery<T> : ICanGetModel, ICanGetService, ICanGetUtility
{
    T Query();
}
```

###### IEvent

```C#
/// <summary>
/// 所有事件都必须继承 IEvent 接口
/// </summary>
public interface IEvent
{
}
```

##### 能做什么接口

###### ICanGetModel

```C#
/// <summary>
/// 可以获取Model层数据
/// </summary>
public interface ICanGetModel
{
}
```

###### ICanGetService

```C#
/// <summary>
/// 可以获取Service层数据
/// </summary>
public interface ICanGetService
{

}
```

###### ICanGetUtility

```C#
/// <summary>
/// 可以获取Utility层数据
/// </summary>
public interface ICanGetUtility
{

}
```

###### ICanCommand

```C#
/// <summary>
/// 可以发送命令
/// </summary>
public interface ICanCommand
{
}
```

###### ICanQuery

```C#
/// <summary>
/// 可以查询
/// </summary>
public interface ICanQuery
{
}
```

###### ICanEvent

```C#
/// <summary>
/// 可以监听、取消监听、触发事件
/// </summary>
public interface ICanEvent
{
}
```



#### Controller层

`Controller层`能做的事取决于`IController`继承了哪些接口

示例代码如下：

```c#
public class Test : MonoBehaviour, IController
{
    void Start()
    {
       this.SendCommand<TestCommand>();
    }
}
```



#### Model层

`Model层`能做的事取决于`IModel`继承了哪些接口

Model默认为数据层，即对数据进行处理。推荐用法如下

1. 先定义**Model**类型接口，如**ITestModel**
2. 再定义**TestModel**类型类，具体实现功能
3. 在注册时通过**ITestModel**注册：TestArchitecture
4. 如需求更改可增加**TestModelPlanB**类实现**ITestModel**，再在注册**ITestModel**的地方更改具体实现类

示例代码如下

- 先定义**Model**类型接口，如**ITestModel**

```c#
// 具体某一Model层接口
public interface ITestModel : IModel
{
    string GetID(string idStr);
}
```

- 再定义**TestModel**类型类，具体实现功能

```
// 具体实现类
public class TestModel : ITestModel
{
    private float id;
    private float mTestID;


    public void Init() // 用于数据初始化
    {
        id = 100000;
       // this.GetModel<TestModelPlanB>(); 不建议在Init中获取层级数据
    }

    string ITestModel.GetID(string idStr) // 推荐显示实现接口
    {
        mTestID = float.Parse(idStr);

        mTestID += id;

        return mTestID.ToString();
    }
}
```

- 在注册时通过**ITestModel**注册：TestArchitecture

```c#
public class TestArchitecture : AbstractArchitecture<TestArchitecture>
{
    protected override void OnInit()
    {
        RegisterModel<ITestModel>(new TestModel());
        //RegisterModel<ITestModel>(new TestModelPlanB());
    }
}
```

- 调用

```c#
public class TestCommand : ICommand
{
    public void Execute()
    {
        string id = this.GetModel<ITestModel>().GetID("0.256");
        Debug.Log($"正在执行Command,TestModel ID 为 : {id}");
    }
}
```



#### Service层

Service层默认为服务层，如实现与服务器交互的代码可以放到这一层中实现。具体的服务实现。推荐用法同Model层。

Model层与Service层的区别：

- Service层：服务型脚本
- Model层：数据型脚本



#### Utility层

Utility层为工具层，里面具体实现自己的工具代码。如对某一字符串进行特殊处理。推荐用法如下：

- 如果项目开发架构统一，可以规范Utility层，所有工具统一从Utility层获取
- 如果项目已经经过许多前辈开发，通用型工具由静态扩展实现，自己的特殊工具由Utility层获取

示例代码如下：

```c#
// 具体某一Utility层接口
public interface ITestUtility : IUtility
{
    int TestGetStringLength(string str);
}
```

```c#
// 具体实现类
public class TestUtility : ITestUtility
{
    int ITestUtility.TestGetStringLength(string str) // 推荐显示实现接口
    {
        return str.Length;
    }
}
```

```c#
// 注册Utility层
public class TestArchitecture : AbstractArchitecture<TestArchitecture>
{
    protected override void OnInit()
    {
        RegisterUtility();
    }
    void RegisterUtility()
    {
        RegisterUtility<ITestUtility>(new TestUtility());
    }
}
```

```c#
// 调用
public class TestCommand : ICommand
{
    public void Execute()
    {
        int length = this.GetModel<ITestUtility>().TestGetStringLength("Hello World");
        Debug.Log($"字符串长度为 : {length}");
    }
}
```



#### Command

必须实现ICommand接口
Execute方法为具体执行命令代码块，当调用SendCommand时会自动执行Execute方法。示例代码如下：

Execute方法执行完时会自动放入对象池，下次使用时会从对象池中获取，不需要new。

> 如果TestCommand中有一些字段，获取TestCommand对象后需要手动初始化

```c#
// 定义 class、struct都可以
public class TestCommand : ICommand
{
    public void Execute()
    {
        int length = this.GetModel<ITestUtility>().TestGetStringLength("Hello World");
        Debug.Log($"字符串长度为 : {length}");
    }
}
```

```c#
// 调用
public class Test : MonoBehaviour, IController
{
    void Start()
    {
       this.SendCommand<TestCommand>();
    }
}
```



#### Query

必须实现IQuery<T>接口。T为返回值类型

Query方法为具体执行查询代码块，当调用DoQuery时会自动执行Query方法。示例代码如下：

Query方法执行完时会自动放入对象池，下次使用时会从对象池中获取，不需要new。

> 如果TestQuery1中有一些字段，获取TestQuery1对象后需要手动初始化

```C#
// 查询调用
public class QueryTest : MonoBehaviour, ICanQuery
{
    void Start()
    {
        int testInt = this.DoQuery<TestQuery1, int>();
        string testBind = this.DoQuery<TestQuery2, string>();
        Debug.Log($"测试Int:{testInt},TestBind:{testBind}");
    }
}
```

```c#
// Query脚本示例
public class TestQuery1 : IQuery<int>
{
    public int Query()
    {
        return this.GetService<ITestService>().TestInt;
    }
}
public class TestQuery2 : IQuery<string>
{
    public string Query()
    {
        return TestBind = this.GetService<ITestService>().TestBind.Value;
    }
}
```

```c#
// 赋值
public interface ITestService : IService
{
    public int TestInt { get; }
    public BindableProperty<string> TestBind { get; }
}
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
```



#### Event使用示例

##### 定义事件

> 引用类型推荐用class
>
> 值类型推荐用struct

```c#
public class TestClassEvent : IEvent
{
    public string Name;
}

public struct TestStructEvent : IEvent
{
    public int ID;
}
```

##### 监听事件、触发事件

```C#
TestClassEvent = this.GetObjInstance<TestClassEvent>(); // 推荐从对象池中获取对象
this.TriggerEvent<TestClassEvent>(); // 触发事件
this.TriggerEvent(TestClassEvent); // 触发事件
```

```c#
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
```



#### 工具

##### Pool对象池示例

> C#对象从对象池获取时，应该对其进行手动初始化，它的值可能为上个放入对象池的值
>
> 物体放入对象池的位置：DontDestroyOnLoad---PoolRoot

```C#
this.GetObjInstance<PoolClassTest>(); // 普通C#类从对象池中获取
this.GetGameObject(prefab); // 物体从对象池中获取

this.PushPool(mPoolClassTest); // C#对象放入对象池
this.PushGameObject(mGoList.First()); // 物体放入对象池
```

```c#
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolTest : MonoBehaviour
{
    public class PoolClassTest
    {
        public int ID = 999;
    }

    private GameObject prefab;
    private PoolClassTest mPoolClassTest;
    private List<GameObject> mGoList = new List<GameObject>();

    private void Start()
    {
        prefab = new GameObject("Prefab");
        prefab.name = prefab.GetInstanceID().ToString();
        mGoList.Add(prefab);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            mPoolClassTest = this.GetObjInstance<PoolClassTest>(); // 从对象池中获取
            Debug.Log($"获取普通C#类_并打印:{mPoolClassTest.ID}");
            mPoolClassTest.ID = 666;

            GameObject go = this.GetGameObject(prefab); // 从对象池中获取
            mGoList.Add(go);
            go.name = go.GetInstanceID().ToString();
            Debug.Log($"获取MonoID_并打印:{go.GetInstanceID()}");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            this.PushPool(mPoolClassTest); // 放入对象池
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            mPoolClassTest = this.GetObjInstance<PoolClassTest>(); // 从对象池中获取
            // 打印值：666,上一个对象的值，所以从对象池中获取到数据后应该对其初始化
            Debug.Log($"获取普通C#类_并打印:{mPoolClassTest.ID}"); 
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            this.PushGameObject(mGoList.First());  // 放入对象池
            mGoList.RemoveAt(0);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            GameObject go = this.GetGameObject(prefab); // 从对象池中获取,prefab为关闭状态则获取到的对象为关闭状态
            mGoList.Add(go);
            go.name = go.GetInstanceID().ToString();
            Debug.Log($"获取MonoID_并打印:{go.GetInstanceID()}");
        }
    }
}
```



##### Singleton

```

```



##### MonoSingleton

```

```



##### Debug

```

```



##### IsNull

```c#

```



##### BindableProperty使用示例

```c#
using UnityEngine;

public class BindablePropertyTest : MonoBehaviour
{
    private BindableProperty<int> bindablePropertyInt = new BindableProperty<int>();
    private BindableProperty<string> bindablePropertyStr;
    
    void Start()
    {
        bindablePropertyInt.mOnValueChanged = newInt =>
        {
            Debug.Log($"新的值：{newInt}");
        };
        bindablePropertyStr = new BindableProperty<string>("InitStr")
        {
            mOnValueChanged = newStr =>
            {
                Debug.Log($"新的字符串：{newStr}");
            }
        };
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            bindablePropertyInt.Value = 1;
            bindablePropertyStr.Value = "Q";
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            bindablePropertyInt.Value = 2;
            bindablePropertyStr.Value = "W";
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            bindablePropertyInt.Value = 3;
            bindablePropertyStr.Value = "E";
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            bindablePropertyInt.Value = 4;
            bindablePropertyStr.Value = "R";
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            bindablePropertyInt.Value = 5;
            bindablePropertyStr.Value = "T";
        }
    }
}

```

