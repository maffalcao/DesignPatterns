namespace MyThreadSafeSingleton;

class MySingleton
{
    private MySingleton() { }

    private static MySingleton _instance;

    // We now have a lock object that will be used to synchronize threads
    // during the first access to the MySingleton.
    private static readonly object _lock = new object();

    public static MySingleton GetInstance(string value)
    {
        // This conditional is necessary to prevent threads from conflicting
        // over the lock when the instance is already created.
        if (_instance == null)
        {
            // Now, picture the program has just started. With no MySingleton
            // instance yet, multiple threads might simultaneously pass the
            // previous conditional and arrive at this point almost at the same time.
            // The first thread will acquire the lock and proceed, while the others
            // will wait here.
            lock (_lock)
            {
                // The first thread to acquire the lock enters this conditional,
                // goes inside, and creates the MySingleton instance. Once it leaves
                // the lock block, a thread that was waiting for the lock release may
                // then enter this section. However, since the MySingleton field is
                // already initialized, the thread won't create a new object.
                if (_instance == null)
                {
                    _instance = new MySingleton();
                    _instance.Value = value;
                }
            }
        }
        return _instance;
    }

    // We'll use this property to demonstrate that our MySingleton indeed works.
    public string Value { get; set; }
}

class MyProgram
{
    static void Main(string[] args)
    {
        // The client code.

        Console.WriteLine(
            "{0}\n{1}\n\n{2}\n",
            "If you see the same value, then the singleton was reused (yay!)",
            "If you see different values, then 2 singletons were created (booo!!)",
            "RESULT:"
        );

        Thread process1 = new Thread(() =>
        {
            TestMySingleton("FOO");
        });
        Thread process2 = new Thread(() =>
        {
            TestMySingleton("BAR");
        });

        process1.Start();
        process2.Start();

        process1.Join();
        process2.Join();
    }

    public static void TestMySingleton(string value)
    {
        MySingleton mySingleton = MySingleton.GetInstance(value);
        Console.WriteLine(mySingleton.Value);
    }
}
