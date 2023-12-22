using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SharedLibrary.Models;

namespace SharedLibrary.Convertors;

public class FieldTypeValueConvertor : ValueConverter<FieldType, int>
{
    private static readonly Expression<Func<FieldType, int>> ConvertFieldTypeToInt
        = fieldType => fieldType.Value;

    private static readonly Expression<Func<int, FieldType>> ConvertIntToFieldType
        = intValue => FieldType.Get(intValue);


    public FieldTypeValueConvertor() : base(ConvertFieldTypeToInt, ConvertIntToFieldType)
    {
    }
}