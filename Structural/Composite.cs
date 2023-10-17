abstract class AbstractComponent
{
    public AbstractComponent() { }

    // The base AbstractComponent may implement some default behavior or leave it to
    // concrete classes (by declaring the method containing the behavior as
    // "abstract").
    public abstract string PerformOperation();

    // In some cases, it would be beneficial to define the child-management
    // operations right in the base AbstractComponent class. This way, you won't
    // need to expose any concrete component classes to the client code,
    // even during the object tree assembly. The downside is that these
    // methods will be empty for the leaf-level components.
    public virtual void AddComponent(AbstractComponent component)
    {
        throw new NotImplementedException();
    }

    public virtual void RemoveComponent(AbstractComponent component)
    {
        throw new NotImplementedException();
    }

    // You can provide a method that lets the client code figure out whether
    // a component can have children.
    public virtual bool IsComposite()
    {
        return true;
    }
}

// The Leaf class represents the end objects of a composition. A leaf can't
// have any children.
//
// Usually, it's the Leaf objects that do the actual work, whereas Composite
// objects only delegate to their sub-components.
class Leaf : AbstractComponent
{
    public override string PerformOperation()
    {
        return "Leaf";
    }

    public override bool IsComposite()
    {
        return false;
    }
}

// The Composite class represents the complex components that may have
// children. Usually, the Composite objects delegate the actual work to
// their children and then "sum-up" the result.
class Composite : AbstractComponent
{
    protected List<AbstractComponent> _children = new List<AbstractComponent>();

    public override void AddComponent(AbstractComponent component)
    {
        this._children.Add(component);
    }

    public override void RemoveComponent(AbstractComponent component)
    {
        this._children.Remove(component);
    }

    // The Composite executes its primary logic in a particular way. It
    // traverses recursively through all its children, collecting and
    // summing their results. Since the composite's children pass these
    // calls to their children and so forth, the whole object tree is
    // traversed as a result.
    public override string PerformOperation()
    {
        int i = 0;
        string result = "Branch(";

        foreach (AbstractComponent component in this._children)
        {
            result += component.PerformOperation();
            if (i != this._children.Count - 1)
            {
                result += "+";
            }
            i++;
        }

        return result + ")";
    }
}

class MyClient
{
    // The client code works with all of the components via the base
    // interface.
    public void UseComponent(AbstractComponent leaf)
    {
        Console.WriteLine($"OUTPUT: {leaf.PerformOperation()}\n");
    }

    // Thanks to the fact that the child-management operations are declared
    // in the base AbstractComponent class, the client code can work with any
    // component, simple or complex, without depending on their concrete
    // classes.
    public void UseComponent2(AbstractComponent component1, AbstractComponent component2)
    {
        if (component1.IsComposite())
        {
            component1.AddComponent(component2);
        }

        Console.WriteLine($"OUTPUT: {component1.PerformOperation()}");
    }
}

class MyApp
{
    static void Main(string[] args)
    {
        MyClient myClient = new MyClient();

        // This way the client code can support the simple leaf
        // components...
        Leaf leaf = new Leaf();
        Console.WriteLine("Client: I get a simple component:");
        myClient.UseComponent(leaf);

        // ...as well as the complex composites.
        Composite tree = new Composite();
        Composite branch1 = new Composite();
        branch1.AddComponent(new Leaf());
        branch1.AddComponent(new Leaf());
        Composite branch2 = new Composite();
        branch2.AddComponent(new Leaf());
        tree.AddComponent(branch1);
        tree.AddComponent(branch2);
        Console.WriteLine("Client: Now I've got a composite tree:");
        myClient.UseComponent(tree);

        Console.Write("Client: I don't need to check the components classes even when managing the tree:\n");
        myClient.UseComponent2(tree, leaf);
    }
}