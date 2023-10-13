namespace MyFactory;

abstract class MyCreator
{
    public abstract IMyProduct CreateProduct();

    public string PerformOperation()
    {
        var product = CreateProduct();
        var result = "MyCreator: The code just worked with " + product.Execute();
        return result;
    }
}

class MyConcreteCreator1 : MyCreator
{
    public override IMyProduct CreateProduct()
    {
        return new MyConcreteProduct1();
    }
}

class MyConcreteCreator2 : MyCreator
{
    public override IMyProduct CreateProduct()
    {
        return new MyConcreteProduct2();
    }
}

public interface IMyProduct
{
    string Execute();
}

class MyConcreteProduct1 : IMyProduct
{
    public string Execute()
    {
        return "Result of MyConcreteProduct1";
    }
}

class MyConcreteProduct2 : IMyProduct
{
    public string Execute()
    {
        return "Result of MyConcreteProduct2";
    }
}

class MyApp
{
    public void Run()
    {
        Console.WriteLine("App: Launched with MyConcreteCreator1.");
        ExecuteClientCode(new MyConcreteCreator1());

        Console.WriteLine("");

        Console.WriteLine("App: Launched with MyConcreteCreator2.");
        ExecuteClientCode(new MyConcreteCreator2());
    }

    public void ExecuteClientCode(MyCreator creator)
    {
        Console.WriteLine("Client: I'm not aware of the creator's class," +
            "but it still works.\n" + creator.PerformOperation());
    }
}

class MyProgram
{
    static void Main(string[] args)
    {
        new MyApp().Run();
    }
}
