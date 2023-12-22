using System.Text.Json;
using System.Text.Json.Serialization;
using SharedLibrary.Models;

namespace SharedLibrary.Convertors;

public class EntityTypeJsonConvertor : JsonConverter<FieldType>
{
    public override FieldType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TryGetInt32(out var intValue)) return FieldType.TryGet(intValue);

        var stringValue = reader.GetString();

        return FieldType.TryGet(stringValue);
    }

    public override void Write(Utf8JsonWriter writer, FieldType value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.DisplayName);
    }
}