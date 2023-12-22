using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using ProgramSelfContainedExtendedEnum.Convertors;

namespace ProgramSelfContainedExtendedEnum.Models;

public class Field
{
    public string Name { get; set; }
    public FieldType Type { get; set; }
}

[JsonConverter(typeof(FieldTypeJsonConvertor))]
public class FieldType : IEquatable<int>
{
    private static readonly List<FieldType> _values = new();

    public static readonly FieldType String = new(0, "String",
        _ => true,
        x => x.ToString());

    public static readonly FieldType Date = new(1, "Date",
        x => DateTime.TryParse(x.ToString(), out _),
        x => DateTime.Parse(x.ToString()).ToString("yyyy-M-d dddd"));

    public static readonly FieldType Int = new(2, "Int",
        x => int.TryParse(x.ToString(), out _));

    public static readonly FieldType Username = new(3, "Username",
        x => Regex.IsMatch(x.ToString(), @"^[a-zA-Z_@][a-zA-Z0-9_]*$"),
        x => $"@{x}");

    public static readonly FieldType PrimeNumber = new(4, "PrimeNumber",
        x => Int.ValidationFunction(x) && IsPrime(int.Parse(x.ToString())));

    private FieldType(int value, string name, Func<object, bool> validationFunction,
        Func<object, string> displayFunction = null)
    {
        Value = value;
        Name = name;
        ValidationFunction = validationFunction;
        DisplayFunction = displayFunction ?? (x => x.ToString());

        _values.Add(this);
    }

    public int Value { get; }
    public string Name { get; }
    public Func<object, bool> ValidationFunction { get; }
    public Func<object, string> DisplayFunction { get; }

    public bool Equals(int other)
    {
        return Value == other;
    }

    public static ReadOnlyCollection<FieldType> GetValues()
    {
        return _values.AsReadOnly();
    }

    public static FieldType? TryGet(int value)
    {
        return _values.FirstOrDefault(x => x.Value == value);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override string ToString()
    {
        return Name;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not FieldType type) return false;

        return Value == type.Value;
    }

    public static bool operator ==(FieldType f1, FieldType f2)
    {
        return f1.Equals(f2);
    }

    public static bool operator !=(FieldType f1, FieldType f2)
    {
        return !(f1 == f2);
    }

    public static implicit operator int(FieldType f)
    {
        return f.Value;
    }

    public static implicit operator FieldType(int value)
    {
        return _values.First(x => x.Value == value);
    }

    private static bool IsPrime(int n) //Method to check prime numbers
    {
        for (var i = 2; i <= Math.Sqrt(n); i++)
            if (n % i == 0)
                return false;
        return true;
    }
}