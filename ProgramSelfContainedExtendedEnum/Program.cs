using System.Text.Json;
using ProgramSelfContainedExtendedEnum.Models;

var json = @"[
            {""Value"": 10, ""Type"": 4},
            {""Value"": 11, ""Type"": 4},
            {""Value"": ""gor8808"", ""Type"": 3},
            {""Value"": ""strValue"", ""Type"": ""Int""}
           ]";

var validItems = JsonSerializer.Deserialize<Item[]>(json)
    .Where(x => x.Type.ValidationFunction(x.Value))
    .Select(x => x.Type.DisplayFunction(x.Value));

Console.WriteLine(string.Join(", ", validItems));

public class Item
{
    public FieldType Type { get; set; }
    public object Value { get; set; }
}


// DisplayValue(15, FieldType.Int);
// DisplayValue("2022/10/15 12:00", FieldType.Date);
// DisplayValue(12, FieldType.PrimeNumber);
// DisplayValue(13, FieldType.PrimeNumber);
// DisplayValue("gor8808", FieldType.Username);
//
// void DisplayValue(object value, FieldType type)
// {
//     if (type.ValidationFunction(value))
//         Console.WriteLine(type.DisplayFunction(value));
//     else
//         Console.WriteLine($"{value} was invalid for type {type}");
// }