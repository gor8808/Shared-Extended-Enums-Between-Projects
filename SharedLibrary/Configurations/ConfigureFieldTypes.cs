using SharedLibrary.Models;

namespace SharedLibrary.Configurations;

public static class ConfigureFieldTypes
{
    public static void Configure(Action<FieldTypeConfiguration> configurationAction)
    {
        var config = new FieldTypeConfiguration();

        configurationAction(config);

        FieldTypeConfiguration.ValidateValues();
    }
}

public class FieldTypeConfiguration
{
    internal FieldTypeConfiguration()
    {
    }

    internal static void ValidateValues()
    {
        if (!FieldType.CheckIfIsInitialized())
            throw new ArgumentException("Field types configuration is missing", nameof(FieldType));
    }

    public FieldTypeConfiguration ConfigureFieldTypes(
        params (int Value, string DisplayName, Func<object, bool> validation)[] values)
    {
        FieldType.Create(values);

        return this;
    }
}