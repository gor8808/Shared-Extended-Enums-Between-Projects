using System.Text.Json;
using System.Text.Json.Serialization;
using ProgramSelfContainedExtendedEnum.Models;

namespace ProgramSelfContainedExtendedEnum.Convertors;

public class FieldTypeJsonConvertor : JsonConverter<FieldType>
{
    public override FieldType? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var values = FieldType.GetValues();
        if (reader.TokenType == JsonTokenType.Number)
        {
            var intValue = reader.GetInt32();
            return values.FirstOrDefault(x => x.Value == intValue);
        }

        if (reader.TokenType == JsonTokenType.String)
        {
            var stringValue = reader.GetString();

            return values.FirstOrDefault(x => x.Name == stringValue);
        }

        throw new InvalidOperationException($"{reader.TokenType} cannot be converted to FieldType");
    }

    public override void Write(Utf8JsonWriter writer, FieldType value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value.Value);
    }
}