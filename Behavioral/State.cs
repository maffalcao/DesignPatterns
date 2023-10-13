namespace NewState;

// The StatePatternDemo defines the interface of interest to clients. It also
// maintains a reference to an instance of a ConcreteState subclass, which
// represents the current state of the StatePatternDemo.
class StatePatternDemo
{
    // A reference to the current state of the StatePatternDemo.
    private ConcreteState _currentState = null;

    public StatePatternDemo(ConcreteState state)
    {
        this.SwitchToState(state);
    }

    // The StatePatternDemo allows changing the ConcreteState object at runtime.
    public void SwitchToState(ConcreteState state)
    {
        Console.WriteLine($"StatePatternDemo: Switch to {state.GetType().Name} state.");
        this._currentState = state;
        this._currentState.SetStatePatternDemo(this);
    }

    // The StatePatternDemo delegates part of its behavior to the current ConcreteState
    // object.
    public void Action1()
    {
        this._currentState.PerformAction1();
    }

    public void Action2()
    {
        this._currentState.PerformAction2();
    }
}

// The base ConcreteState class declares methods that all ConcreteState should
// implement and provides a backreference to the StatePatternDemo object.
// This backreference can be used by ConcreteStates to transition the StatePatternDemo
// to another state.
abstract class ConcreteState
{
    protected StatePatternDemo _statePatternDemo;

    public void SetStatePatternDemo(StatePatternDemo statePatternDemo)
    {
        this._statePatternDemo = statePatternDemo;
    }

    public abstract void PerformAction1();

    public abstract void PerformAction2();
}

// Concrete ConcreteStates implement various behaviors associated with a state of
// the StatePatternDemo.
class StateA : ConcreteState
{
    public override void PerformAction1()
    {
        Console.WriteLine("StateA handles action1.");
        Console.WriteLine("StateA wants to switch to another state.");
        this._statePatternDemo.SwitchToState(new StateB());
    }

    public override void PerformAction2()
    {
        Console.WriteLine("StateA handles action2.");
    }
}

class StateB : ConcreteState
{
    public override void PerformAction1()
    {
        Console.Write("StateB handles action1.");
    }

    public override void PerformAction2()
    {
        Console.WriteLine("StateB handles action2.");
        Console.WriteLine("StateB wants to switch to another state.");
        this._statePatternDemo.SwitchToState(new StateA());
    }
}

class Program
{
    static void Main(string[] args)
    {
        // The client code.
        var statePatternDemo = new StatePatternDemo(new StateA());
        statePatternDemo.Action1();
        statePatternDemo.Action2();
    }
}
