using System.Text.RegularExpressions;
using ProgramSelfContained.Models;

namespace ProgramSelfContained.Services;

public class FieldService
{
    public static bool IsFieldValueValid(Field field, object fieldValue)
    {
        return field.Type switch
        {
            FieldType.String => true,
            FieldType.Date => DateTime.TryParse(fieldValue.ToString(), out _),
            FieldType.Int => int.TryParse(fieldValue.ToString(), out _),
            FieldType.Username => Regex.IsMatch(fieldValue.ToString(), "@\"^[a-zA-Z_@][a-zA-Z0-9_]*$\""),
            FieldType.PrimeNumber => int.TryParse(fieldValue.ToString(), out var intValue) && IsPrime(intValue),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private static bool IsPrime(int n) //Method to check prime numbers
    {
        for (var i = 2; i <= Math.Sqrt(n); i++)
            if (n % i == 0)
                return false;
        return true;
    }
}