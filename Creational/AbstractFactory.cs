namespace MyFactoryConcept;

// The Abstract Factory interface declares a set of methods that return
// different abstract products. These products are called a family and are
// related by a high-level theme or concept. Products of one family are
// usually able to collaborate among themselves. A family of products may
// have several variants, but the products of one variant are incompatible
// with products of another.
public interface IMyAbstractFactory
{
    IMyAbstractProductA CreateProductA();

    IMyAbstractProductB CreateProductB();
}

// Concrete Factories produce a family of products that belong to a single
// variant. The factory guarantees that resulting products are compatible.
// Note that signatures of the Concrete Factory's methods return an abstract
// product, while inside the method a concrete product is instantiated.
class MyConcreteFactory1 : IMyAbstractFactory
{
    public IMyAbstractProductA CreateProductA()
    {
        return new MyConcreteProductA1();
    }

    public IMyAbstractProductB CreateProductB()
    {
        return new MyConcreteProductB1();
    }
}

// Each Concrete Factory has a corresponding product variant.
class MyConcreteFactory2 : IMyAbstractFactory
{
    public IMyAbstractProductA CreateProductA()
    {
        return new MyConcreteProductA2();
    }

    public IMyAbstractProductB CreateProductB()
    {
        return new MyConcreteProductB2();
    }
}

// Each distinct product of a product family should have a base interface.
// All variants of the product must implement this interface.
public interface IMyAbstractProductA
{
    string PerformFunctionA();
}

// Concrete Products are created by corresponding Concrete Factories.
class MyConcreteProductA1 : IMyAbstractProductA
{
    public string PerformFunctionA()
    {
        return "Product A1's result.";
    }
}

class MyConcreteProductA2 : IMyAbstractProductA
{
    public string PerformFunctionA()
    {
        return "Product A2's result.";
    }
}

// Here's the base interface of another product. All products can
// interact with each other, but proper interaction is possible only between
// products of the same concrete variant.
public interface IMyAbstractProductB
{
    string PerformFunctionB();

    string CollaborateWithA(IMyAbstractProductA collaborator);
}

// Concrete Products are created by corresponding Concrete Factories.
class MyConcreteProductB1 : IMyAbstractProductB
{
    public string PerformFunctionB()
    {
        return "Product B1's result.";
    }

    public string CollaborateWithA(IMyAbstractProductA collaborator)
    {
        var result = collaborator.PerformFunctionA();

        return $"B1 collaborating with ({result})";
    }
}

class MyConcreteProductB2 : IMyAbstractProductB
{
    public string PerformFunctionB()
    {
        return "Product B2's result.";
    }

    public string CollaborateWithA(IMyAbstractProductA collaborator)
    {
        var result = collaborator.PerformFunctionA();

        return $"B2 collaborating with ({result})";
    }
}

// The client code works with factories and products only through abstract
// types: AbstractFactory and AbstractProduct. This allows you to pass any
// factory or product subclass to the client code without breaking it.
class MyClient
{
    public void Execute()
    {
        // The client code can work with any concrete factory class.
        Console.WriteLine("Client: Testing client code with the first factory type...");
        RunClient(new MyConcreteFactory1());
        Console.WriteLine();

        Console.WriteLine("Client: Testing the same client code with the second factory type...");
        RunClient(new MyConcreteFactory2());
    }

    public void RunClient(IMyAbstractFactory factory)
    {
        var productA = factory.CreateProductA();
        var productB = factory.CreateProductB();

        Console.WriteLine(productB.PerformFunctionB());
        Console.WriteLine(productB.CollaborateWithA(productA));
    }
}

class MyProgram
{
    static void Main(string[] args)
    {
        new MyClient().Execute();
    }
}
