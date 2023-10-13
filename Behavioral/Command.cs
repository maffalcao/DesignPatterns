// The CommandPatternDemo interface declares a method for executing a command.
public interface ICommandPatternDemo
{
    void Execute();
}

// Some commands can implement simple operations on their own.
class SimpleCommandPatternDemo : ICommandPatternDemo
{
    private string _data = string.Empty;

    public SimpleCommandPatternDemo(string data)
    {
        this._data = data;
    }

    public void Execute()
    {
        Console.WriteLine($"SimpleCommandPatternDemo: I can perform simple actions like printing ({this._data})");
    }
}

// However, some commands can delegate more complex operations to other
// objects, called "receivers."
class ComplexCommandPatternDemo : ICommandPatternDemo
{
    private ReceiverPatternDemo _receiver;

    // Context data, required for launching the receiver's methods.
    private string _inputA;
    private string _inputB;

    // Complex commands can accept one or several receiver objects along
    // with any context data via the constructor.
    public ComplexCommandPatternDemo(ReceiverPatternDemo receiver, string inputA, string inputB)
    {
        this._receiver = receiver;
        this._inputA = inputA;
        this._inputB = inputB;
    }

    // Commands can delegate to any methods of a receiver.
    public void Execute()
    {
        Console.WriteLine("ComplexCommandPatternDemo: Complex tasks are handled by a receiver object.");
        this._receiver.DoTaskA(this._inputA);
        this._receiver.DoTaskB(this._inputB);
    }
}

// The ReceiverPatternDemo class contains important business logic. It knows how
// to perform all kinds of operations, associated with fulfilling a request.
class ReceiverPatternDemo
{
    public void DoTaskA(string inputA)
    {
        Console.WriteLine($"ReceiverPatternDemo: Working on Task A ({inputA}.)");
    }

    public void DoTaskB(string inputB)
    {
        Console.WriteLine($"ReceiverPatternDemo: Also working on Task B ({inputB}.)");
    }
}

// The InvokerPatternDemo is associated with one or several commands. It sends a
// request to the command.
class InvokerPatternDemo
{
    private ICommandPatternDemo _onStart;
    private ICommandPatternDemo _onFinish;

    // Initialize commands.
    public void SetOnStart(ICommandPatternDemo command)
    {
        this._onStart = command;
    }

    public void SetOnFinish(ICommandPatternDemo command)
    {
        this._onFinish = command;
    }

    // The InvokerPatternDemo does not depend on concrete command or receiver classes.
    // The InvokerPatternDemo passes a request to a receiver indirectly, by executing a command.
    public void PerformImportantTask()
    {
        Console.WriteLine("InvokerPatternDemo: Is there anything to do before I start?");
        if (this._onStart is ICommandPatternDemo)
        {
            this._onStart.Execute();
        }

        Console.WriteLine("InvokerPatternDemo: ...doing something truly significant...");

        Console.WriteLine("InvokerPatternDemo: Is there anything to do after I finish?");
        if (this._onFinish is ICommandPatternDemo)
        {
            this._onFinish.Execute();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // The client code can parameterize an invoker with any commands.
        InvokerPatternDemo invoker = new InvokerPatternDemo();
        invoker.SetOnStart(new SimpleCommandPatternDemo("Greetings!"));
        ReceiverPatternDemo receiver = new ReceiverPatternDemo();
        invoker.SetOnFinish(new ComplexCommandPatternDemo(receiver, "Send notifications", "Generate report"));

        invoker.PerformImportantTask();
    }
}
