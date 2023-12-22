using Microsoft.EntityFrameworkCore;
using SharedLibrary.Configurations;
using SharedLibrary.DataAccess;
using SharedLibrary.Models;

namespace ExtendedEnum;

internal class Program
{
    private static async Task Main(string[] args)
    {
        FieldTypeConfigurations.Configure(config =>
        {
            config.ConfigureFieldTypes(
                (0, "string", x => true),
                (1, "boolean", x => bool.TryParse(x.ToString(), out _))
            );
        });

        var context = new AppDbContext();

        await context.Database.MigrateAsync();

        var fields = await context.Fields.Where(x => x.Type == FieldType.Get("boolean"))
            .ToListAsync();

        Console.WriteLine($"Found {fields.Count} fields with boolean type");
    }
}