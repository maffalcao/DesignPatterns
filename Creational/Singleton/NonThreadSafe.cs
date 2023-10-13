namespace MySingleton;

public sealed class MySingleton
{
    // The Singleton's constructor should always be private to prevent
    // direct construction calls with the `new` operator.
    private MySingleton() { }

    // The Singleton's instance is stored in a static field. There are
    // multiple ways to initialize this field, each with various pros
    // and cons. In this example, we'll demonstrate one of the simplest ways,
    // which, however, is not thread-safe.
    private static MySingleton _instance;

    // This is the static method that controls access to the singleton
    // instance. On the first run, it creates a singleton object and stores
    // it in the static field. On subsequent runs, it returns the existing
    // object from the static field.
    public static MySingleton GetInstance()
    {
        if (_instance == null)
        {
            _instance = new MySingleton();
        }
        return _instance;
    }

    // Finally, any singleton should define some business logic, which can
    // be executed on its instance.
    public void PerformBusinessLogic()
    {
        // ...
    }
}

class MyProgram
{
    static void Main(string[] args)
    {
        // The client code.
        MySingleton s1 = MySingleton.GetInstance();
        MySingleton s2 = MySingleton.GetInstance();

        if (s1 == s2)
        {
            Console.WriteLine("MySingleton works; both variables contain the same instance.");
        }
        else
        {
            Console.WriteLine("MySingleton failed; variables contain different instances.");
        }
    }
}
