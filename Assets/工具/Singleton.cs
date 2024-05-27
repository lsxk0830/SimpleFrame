public class Singleton<T> where T : class, new()
{
    private static T instance = System.Activator.CreateInstance<T>();

    public static T Instance
    {
        get
        {
            return instance;
        }
        protected set
        {
            instance = value;
        }
    }
}