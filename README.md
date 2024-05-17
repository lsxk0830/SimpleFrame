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

Model默认为数据层，即对数据进行处理。推荐用法如下

1. 先定义**Model**类型接口，如**ITestModel**
2. 再定义**TestModel**类型类，具体实现功能
3. 在注册时通过**ITestModel**注册：TestArchitecture
4. 如需求更改可增加**TestModelPlanB**类实现**ITestModel**，再在注册**ITestModel**的地方更改具体实现类

示例代码如下

- 先定义**Model**类型接口，如**ITestModel**

```c#
public interface ITestModel : IModel
{
    string GetID(string idStr);
}
```

- 再定义**TestModel**类型类，具体实现功能

```
public class TestModel : ITestModel
{
    private float id;
    private float mTestID;


    public void Init() // 用于数据初始化
    {
        id = 100000;
       // this.GetModel<TestModelPlanB>(); 不建议在Init中获取层级数据
    }

    string ITestModel.GetID(string idStr)
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
        //RegisterModel<ITestModel>(new **TestModelPlanB**());
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



```c#

```



#### Utility层



```c#

```



#### Command



```c#

```



#### Query



```c#

```



#### Event



```c#

```



#### 工具

##### Pool



```c#

```



##### Bindpro

