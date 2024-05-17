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