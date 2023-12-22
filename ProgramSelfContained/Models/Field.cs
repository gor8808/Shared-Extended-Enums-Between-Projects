namespace ProgramSelfContained.Models;

public class Field
{
    public string Name { get; set; }
    public FieldType Type { get; set; }
}

public enum FieldType
{
    String,
    Date,
    Int,
    Username,
    PrimeNumber
}