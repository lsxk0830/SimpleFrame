using UnityEngine;

public class TestCommand : ICommand
{
    public void Execute()
    {
        string id = this.GetModel<ITestModel>().GetID("0.256");
        Debug.Log($"正在执行Command,TestModel ID 为 : {id}");
    }
}