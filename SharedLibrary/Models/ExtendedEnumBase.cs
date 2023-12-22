namespace SharedLibrary.Models;

public class ExtendedEnumBase : IComparable
{
    protected ExtendedEnumBase(string name, int value)
    {
        Name = name;
        Value = value;
    }

    public string Name { get; protected set; }
    public int Value { get; protected set; }

    public int CompareTo(object obj)
    {
        return Value.CompareTo(obj as ExtendedEnumBase);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not ExtendedEnumBase otherValue) return false;

        return otherValue.Value == Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override string ToString()
    {
        return Name;
    }
}