namespace SimplifiedFacade;

// The SimplifiedFacade class offers an easy-to-use interface to the complex operations
// of one or more subsystems. It delegates client requests to the appropriate objects
// within the subsystem and manages their lifecycle. This shields clients from the
// intricate complexity of the subsystem.
public class SimplifiedFacade
{
    protected SubsystemA _subsystemA;
    protected SubsystemB _subsystemB;

    public SimplifiedFacade(SubsystemA subsystemA, SubsystemB subsystemB)
    {
        this._subsystemA = subsystemA;
        this._subsystemB = subsystemB;
    }

    // Methods in the SimplifiedFacade provide simplified access to the advanced
    // capabilities of the subsystems. However, clients only access a fraction of
    // a subsystem's functionality.
    public string PerformOperations()
    {
        string result = "SimplifiedFacade initializes subsystems:\n";
        result += this._subsystemA.Operation1();
        result += this._subsystemB.Operation1();
        result += "SimplifiedFacade instructs subsystems to perform actions:\n";
        result += this._subsystemA.OperationN();
        result += this._subsystemB.OperationZ();
        return result;
    }
}

// The Subsystem can handle requests from both the facade and the client directly.
// To the Subsystem, the SimplifiedFacade is just another client and not part of
// the Subsystem itself.
public class SubsystemA
{
    public string Operation1()
    {
        return "SubsystemA: Ready!\n";
    }

    public string OperationN()
    {
        return "SubsystemA: Go!\n";
    }
}

// Some facades can work with multiple subsystems simultaneously.
public class SubsystemB
{
    public string Operation1()
    {
        return "SubsystemB: Get ready!\n";
    }

    public string OperationZ()
    {
        return "SubsystemB: Fire!\n";
    }
}

class Client
{
    // The client code interacts with complex subsystems through a user-friendly
    // interface provided by the SimplifiedFacade. When the facade manages the
    // subsystem's lifecycle, the client may not even be aware of the subsystem's existence.
    // This approach keeps complexity in check.
    public static void UseSimplifiedFacade(SimplifiedFacade facade)
    {
        Console.Write(facade.PerformOperations());
    }
}

class Program
{
    static void Main(string[] args)
    {
        // The client code may already have some subsystem objects created. In such cases,
        // it can be beneficial to initialize the SimplifiedFacade with these objects,
        // rather than allowing the facade to create new instances.
        SubsystemA subsystemA = new SubsystemA();
        SubsystemB subsystemB = new SubsystemB();
        SimplifiedFacade facade = new SimplifiedFacade(subsystemA, subsystemB);
        Client.UseSimplifiedFacade(facade);
    }
}
