namespace MyEasyFacade;

// The EasyFacade class offers a user-friendly interface to the complex operations
// of one or more subsystems. It delegates client requests to the appropriate objects
// within the subsystem and manages their lifecycle. This shields clients from the
// intricate complexity of the subsystem.
public class MyEasyFacade
{
    protected MySubsystemA _subsystemA;
    protected MySubsystemB _subsystemB;

    public MyEasyFacade(MySubsystemA subsystemA, MySubsystemB subsystemB)
    {
        this._subsystemA = subsystemA;
        this._subsystemB = subsystemB;
    }

    // Methods in the EasyFacade provide simplified access to the advanced
    // capabilities of the subsystems. However, clients only access a fraction of
    // a subsystem's functionality.
    public string ExecuteOperations()
    {
        string result = "MyEasyFacade initializes subsystems:\n";
        result += this._subsystemA.Operate1();
        result += this._subsystemB.Operate1();
        result += "MyEasyFacade instructs subsystems to perform actions:\n";
        result += this._subsystemA.OperateN();
        result += this._subsystemB.OperateZ();
        return result;
    }
}

// The Subsystem can handle requests from both the facade and the client directly.
// To the Subsystem, the EasyFacade is just another client and not part of
// the Subsystem itself.
public class MySubsystemA
{
    public string Operate1()
    {
        return "MySubsystemA: Ready!\n";
    }

    public string OperateN()
    {
        return "MySubsystemA: Go!\n";
    }
}

// Some facades can work with multiple subsystems simultaneously.
public class MySubsystemB
{
    public string Operate1()
    {
        return "MySubsystemB: Get ready!\n";
    }

    public string OperateZ()
    {
        return "MySubsystemB: Fire!\n";
    }
}

class MyClient
{
    // The client code interacts with complex subsystems through a user-friendly
    // interface provided by the EasyFacade. When the facade manages the
    // subsystem's lifecycle, the client may not even be aware of the subsystem's existence.
    // This approach keeps complexity in check.
    public static void UseMyEasyFacade(MyEasyFacade facade)
    {
        Console.Write(facade.ExecuteOperations());
    }
}

class MyProgram
{
    static void Main(string[] args)
    {
        // The client code may already have some subsystem objects created. In such cases,
        // it can be beneficial to initialize the EasyFacade with these objects,
        // rather than allowing the facade to create new instances.
        MySubsystemA subsystemA = new MySubsystemA();
        MySubsystemB subsystemB = new MySubsystemB();
        MyEasyFacade facade = new MyEasyFacade(subsystemA, subsystemB);
        MyClient.UseMyEasyFacade(facade);
    }
}
