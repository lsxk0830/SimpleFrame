namespace Blue
{
    public class SingletonMonobehaviour<T> : AbstractController where T:SingletonMonobehaviour<T>
    {
        private static T _instance;
        public static T Instance => _instance;

        public virtual void OnAwake() { }
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this.GetComponent<T>();
                _instance.OnAwake();
                DontDestroyOnLoad(_instance);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
