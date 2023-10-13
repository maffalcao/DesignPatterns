namespace NewModifiedDecorator;

// The Component base class defines the interface for components that can be
// extended by decorators.
public abstract class MyComponent
{
    public abstract string PerformTask();
}

// Concrete Components provide default implementations of the operations.
class MyConcreteComponent : MyComponent
{
    public override string PerformTask()
    {
        return "ConcreteComponent";
    }
}

// The Decorator base class follows the same interface as other
// components. This class primarily defines the wrapping interface for all
// concrete decorators. The default implementation of the wrapping code
// includes a field to store a wrapped component and the means to initialize it.
abstract class MyDecorator : MyComponent
{
    protected MyComponent _component;

    public MyDecorator(MyComponent component)
    {
        this._component = component;
    }

    public void SetComponent(MyComponent component)
    {
        this._component = component;
    }

    // The Decorator delegates all work to the wrapped component.
    public override string PerformTask()
    {
        if (this._component != null)
        {
            return this._component.PerformTask();
        }
        else
        {
            return string.Empty;
        }
    }
}

// Concrete Decorators invoke the wrapped object and modify its result in some way.
class MyConcreteDecoratorA : MyDecorator
{
    public MyConcreteDecoratorA(MyComponent component) : base(component)
    {
    }

    // Decorators can choose to call the parent implementation of the operation
    // instead of calling the wrapped object directly. This approach simplifies
    // the extension of decorator classes.
    public override string PerformTask()
    {
        return $"ConcreteDecoratorA({base.PerformTask()})";
    }
}

// Decorators can execute their behavior either before or after the call to
// a wrapped object.
class MyConcreteDecoratorB : MyDecorator
{
    public MyConcreteDecoratorB(MyComponent component) : base(component)
    {
    }

    public override string PerformTask()
    {
        return $"ConcreteDecoratorB({base.PerformTask()})";
    }
}

public class MyClient
{
    // The client code works with all objects using the Component interface.
    // This way, it can remain independent of the concrete classes of
    // components it interacts with.
    public void Execute(MyComponent component)
    {
        Console.WriteLine("RESULT: " + component.PerformTask());
    }
}

class MyProgram
{
    static void Main(string[] args)
    {
        MyClient client = new MyClient();

        var simple = new MyConcreteComponent();
        Console.WriteLine("Client: I have a simple component:");
        client.Execute(simple);
        Console.WriteLine();

        // ...as well as decorated ones.
        //
        // Notice how decorators can wrap not only simple components but also

    }
}
