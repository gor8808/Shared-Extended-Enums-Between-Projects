using System.ComponentModel;
using System.Text.Json.Serialization;
using SharedLibrary.Convertors;

namespace SharedLibrary.Models;

[JsonConverter(typeof(FieldTypeJsonConvertor))]
[TypeConverter(typeof(FieldTypeTypeConverter))]
public class FieldType : ExtendedEnumBase, IComparable
{
    private static HashSet<FieldType>? _fieldTypes;

    private FieldType(int value, string name, Func<object, bool> validationFunction) : base(name, value)
    {
        ValidationFunction = validationFunction;
    }

    [JsonIgnore] public static IEnumerable<FieldType>? Values => _fieldTypes;

    [JsonIgnore] public Func<object, bool> ValidationFunction { get; }

    public static bool operator ==(FieldType? left, FieldType? right)
    {
        return left?.Value == right?.Value;
    }

    public static bool operator !=(FieldType? left, FieldType? right)
    {
        return !(left == right);
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public static FieldType TryGet(int value)
    {
        CheckToBeInitialized();

        return _fieldTypes.FirstOrDefault(x => x.Value == value);
    }

    public static FieldType TryGet(string displayName)
    {
        CheckToBeInitialized();

        return _fieldTypes.FirstOrDefault(x =>
            string.Equals(x.Name, displayName, StringComparison.OrdinalIgnoreCase));
    }

    public static FieldType Get(int value)
    {
        CheckToBeInitialized();

        return _fieldTypes.First(x => x.Value == value);
    }

    public static FieldType Get(string displayName)
    {
        CheckToBeInitialized();

        return _fieldTypes.First(x => string.Equals(x.Name, displayName, StringComparison.OrdinalIgnoreCase));
    }

    public static void Create(params (int value, string displayName, Func<object, bool> validation)[] values)
    {
        //If is not already initialized
        if (CheckIfIsInitialized()) return;

        values = values.OrderBy(x => x.value).ToArray();

        _fieldTypes = new HashSet<FieldType>(values.Length);

        foreach (var value in values)
        {
            var appRule = new FieldType(value.value, value.displayName, value.validation);
            _fieldTypes.Add(appRule);
        }
    }

    public static bool CheckIfIsInitialized()
    {
        return _fieldTypes != null;
    }

    private static void CheckToBeInitialized()
    {
        if (!CheckIfIsInitialized())
            throw new InvalidOperationException("Field types must be initialized using FieldTypes.Create() method");
    }
}