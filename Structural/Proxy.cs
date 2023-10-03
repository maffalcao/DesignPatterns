namespace StructuralPatterns.ProxyExample;

// A shared interface for both RealEntity and Proxy, enabling clients to work with either of them.
public interface IEntity
{
    void PerformRequest();
}

// RealEntity contains the core business logic, which may be resource-intensive or sensitive.
// The Proxy can enhance or control access to this logic without altering RealEntity.
class RealEntity : IEntity
{
    public void PerformRequest()
    {
        Console.WriteLine("RealEntity: Handling the request.");
    }
}

// The Proxy implements the same interface as RealEntity.
class Proxy : IEntity
{
    private RealEntity _realEntity;

    public Proxy(RealEntity realEntity)
    {
        this._realEntity = realEntity;
    }

    // The Proxy pattern is often used for lazy loading, caching, access control, and logging.
    // It can delegate the request to RealEntity after performing these tasks.
    public void PerformRequest()
    {
        if (this.CheckAccess())
        {
            this._realEntity.PerformRequest();
            this.LogAccess();
        }
    }

    public bool CheckAccess()
    {
        // Actual access checks should be implemented here.
        Console.WriteLine("Proxy: Checking access before forwarding the request.");
        return true;
    }

    public void LogAccess()
    {
        Console.WriteLine("Proxy: Logging the request timestamp.");
    }
}

public class Client
{
    // The client code interacts with objects via the IEntity interface,
    // supporting both RealEntities and Proxies.
    public void ClientLogic(IEntity entity)
    {
        // ...

        entity.PerformRequest();

        // ...
    }
}

class Program
{
    static void Main(string[] args)
    {
        Client client = new Client();

        Console.WriteLine("Client: Executing the client code with a real entity:");
        RealEntity realEntity = new RealEntity();
        client.ClientLogic(realEntity);

        Console.WriteLine();

        Console.WriteLine("Client: Executing the same client code with a proxy:");
        Proxy proxy = new Proxy(realEntity);
        client.ClientLogic(proxy);
    }
}