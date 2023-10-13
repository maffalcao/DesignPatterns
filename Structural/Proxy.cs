namespace MyProxyExample;

// A common interface for both RealObject and Proxy, allowing clients to work with either of them.
public interface IMyEntity
{
    void ExecuteRequest();
}

// RealObject contains the core business logic, which can be resource-intensive or sensitive.
// The Proxy can enhance or control access to this logic without modifying RealObject.
class MyRealObject : IMyEntity
{
    public void ExecuteRequest()
    {
        Console.WriteLine("MyRealObject: Handling the request.");
    }
}

// The Proxy implements the same interface as RealObject.
class MyProxy : IMyEntity
{
    private MyRealObject _realObject;

    public MyProxy(MyRealObject realObject)
    {
        this._realObject = realObject;
    }

    // The Proxy pattern is often used for lazy loading, caching, access control, and logging.
    // It can delegate the request to RealObject after performing these tasks.
    public void ExecuteRequest()
    {
        if (this.CheckAccess())
        {
            this._realObject.ExecuteRequest();
            this.LogAccess();
        }
    }

    public bool CheckAccess()
    {
        // Actual access checks should be implemented here.
        Console.WriteLine("MyProxy: Checking access before forwarding the request.");
        return true;
    }

    public void LogAccess()
    {
        Console.WriteLine("MyProxy: Logging the request timestamp.");
    }
}

public class MyClient
{
    // The client code interacts with objects through the IMyEntity interface,
    // supporting both RealObjects and Proxies.
    public void ClientLogic(IMyEntity entity)
    {
        // ...

        entity.ExecuteRequest();

        // ...
    }
}

class MyProgram
{
    static void Main(string[] args)
    {
        MyClient client = new MyClient();

        Console.WriteLine("Client: Running the client code with a real object:");
        MyRealObject realObject = new MyRealObject();
        client.ClientLogic(realObject);

        Console.WriteLine();

        Console.WriteLine("Client: Running the same client code with a proxy:");
        MyProxy proxy = new MyProxy(realObject);
        client.ClientLogic(proxy);
    }
}
