using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using SharedLibrary.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SharedLibrary.Configurations;

public static class FieldTypeConfigurations
{
    public static void Configure(Action<FieldTypeConfigurator> configurationAction)
    {
        var config = new FieldTypeConfigurator();

        configurationAction(config);

        FieldTypeConfigurator.ValidateValues();
    }

    public static void ConfigureSharedLibrary(this IServiceCollection services,
        Action<FieldTypeConfigurator> configurationAction)
    {
        var config = new FieldTypeConfigurator();

        configurationAction(config);

        FieldTypeConfigurator.ValidateValues();

        services.ConfigureSwaggerGen(c =>
        {
            //Add schema filter for class to show as enum values in swagger
            c.SchemaFilter<FieldTypeSchemaFilter>();
        });
    }
}

public class FieldTypeSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type != typeof(FieldType)) return;

        schema.Type = "string";
        schema.AdditionalPropertiesAllowed = true;
        schema.Description = null;
        schema.Properties = new Dictionary<string, OpenApiSchema>();

        if (!context.SchemaRepository.Schemas.ContainsKey(nameof(FieldType)))
            schema.Enum = FieldType.Values!.Select(v => new OpenApiString(v.Value.ToString()))
                .ToList<IOpenApiAny>();
    }
}

public class FieldTypeConfigurator
{
    internal FieldTypeConfigurator()
    {
    }

    internal static void ValidateValues()
    {
        if (!FieldType.CheckIfIsInitialized())
            throw new ArgumentException("Field types configuration is missing", nameof(FieldType));
    }

    public FieldTypeConfigurator ConfigureFieldTypes(
        params (int Value, string DisplayName, Func<object, bool> validation)[] values)
    {
        FieldType.Create(values);

        return this;
    }
}