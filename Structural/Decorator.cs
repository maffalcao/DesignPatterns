namespace ModifiedDecorator;

// The base Component interface defines operations that can be extended by
// decorators.
public abstract class Component
{
    public abstract string PerformOperation();
}

// Concrete Components provide default implementations of the operations.
// There may be several variations of these classes.
class ConcreteComponent : Component
{
    public override string PerformOperation()
    {
        return "ConcreteComponent";
    }
}

// The base Decorator class follows the same interface as other
// components. This class primarily defines the wrapping interface for all
// concrete decorators. The default implementation of the wrapping code
// includes a field to store a wrapped component and the means to initialize it.
abstract class Decorator : Component
{
    protected Component _component;

    public Decorator(Component component)
    {
        this._component = component;
    }

    public void SetComponent(Component component)
    {
        this._component = component;
    }

    // The Decorator delegates all work to the wrapped component.
    public override string PerformOperation()
    {
        if (this._component != null)
        {
            return this._component.PerformOperation();
        }
        else
        {
            return string.Empty;
        }
    }
}

// Concrete Decorators invoke the wrapped object and modify its result in some way.
class ConcreteDecoratorA : Decorator
{
    public ConcreteDecoratorA(Component component) : base(component)
    {
    }

    // Decorators can choose to call the parent implementation of the operation
    // instead of calling the wrapped object directly. This approach simplifies
    // the extension of decorator classes.
    public override string PerformOperation()
    {
        return $"ConcreteDecoratorA({base.PerformOperation()})";
    }
}

// Decorators can execute their behavior either before or after the call to
// a wrapped object.
class ConcreteDecoratorB : Decorator
{
    public ConcreteDecoratorB(Component component) : base(component)
    {
    }

    public override string PerformOperation()
    {
        return $"ConcreteDecoratorB({base.PerformOperation()})";
    }
}

public class Client
{
    // The client code works with all objects using the Component interface.
    // This way, it can remain independent of the concrete classes of
    // components it interacts with.
    public void ClientCode(Component component)
    {
        Console.WriteLine("RESULT: " + component.PerformOperation());
    }
}

class Program
{
    static void Main(string[] args)
    {
        Client client = new Client();

        var simple = new ConcreteComponent();
        Console.WriteLine("Client: I have a simple component:");
        client.ClientCode(simple);
        Console.WriteLine();

        // ...as well as decorated ones.
        //
        // Notice how decorators can wrap not only simple components but also

    }
}