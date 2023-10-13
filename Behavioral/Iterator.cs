namespace NewIterator;

using System.Collections;

abstract class IteratorPatternDemo : IEnumerator
{
    object IEnumerator.Current => GetCurrent();

    // Get the key of the current element
    public abstract int GetKey();

    // Get the current element
    public abstract object GetCurrent();

    // Move to the next element
    public abstract bool MoveToNext();

    // Rewind the iterator to the first element
    public abstract void ResetIterator();
}

abstract class IteratorAggregatePatternDemo : IEnumerable
{
    // Get an Iterator or another IteratorAggregate for the implementing object.
    public abstract IEnumerator GetEnumerator();
}

// Concrete Iterators implement various traversal algorithms. These classes
// maintain the current traversal position at all times.
class AlphabeticalOrderIteratorPatternDemo : IteratorPatternDemo
{
    private WordsCollectionPatternDemo _collection;

    // Stores the current traversal position. An iterator may have other fields
    // for maintaining iteration state, especially for working with specific
    // types of collections.
    private int _position = -1;

    private bool _reverse = false;

    public AlphabeticalOrderIteratorPatternDemo(WordsCollectionPatternDemo collection, bool reverse = false)
    {
        _collection = collection;
        _reverse = reverse;

        if (reverse)
        {
            _position = collection.GetItems().Count;
        }
    }

    public override object GetCurrent()
    {
        return this._collection.GetItems()[_position];
    }

    public override int GetKey()
    {
        return this._position;
    }

    public override bool MoveToNext()
    {
        int updatedPosition = this._position + (this._reverse ? -1 : 1);

        if (updatedPosition >= 0 && updatedPosition < this._collection.GetItems().Count)
        {
            this._position = updatedPosition;
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void ResetIterator()
    {
        this._position = this._reverse ? this._collection.GetItems().Count - 1 : 0;
    }
}

// Concrete Collections provide one or more methods for getting new
// iterator instances compatible with the collection class.
class WordsCollectionPatternDemo : IteratorAggregatePatternDemo
{
    List<string> _collection = new List<string>();

    bool _direction = false;

    public void ReverseDirection()
    {
        _direction = !_direction;
    }

    public List<string> GetItems()
    {
        return _collection;
    }

    public void AddItem(string item)
    {
        this._collection.Add(item);
    }

    public override IEnumerator GetEnumerator()
    {
        return new AlphabeticalOrderIteratorPatternDemo(this, _direction);
    }
}

class Program
{
    static void Main(string[] args)
    {
        // The client code may or may not know about the Concrete Iterator
        // or Collection classes, depending on the level of indirection you
        // want to maintain in your program.
        var collection = new WordsCollectionPatternDemo();
        collection.AddItem("First");
        collection.AddItem("Second");
        collection.AddItem("Third");

        Console.WriteLine("Forward traversal:");

        foreach (var element in collection)
        {
            Console.WriteLine(element);
        }

        Console.WriteLine("\nReverse traversal:");

        collection.ReverseDirection();

        foreach (var element in collection)
        {
            Console.WriteLine(element);
        }
    }
}
