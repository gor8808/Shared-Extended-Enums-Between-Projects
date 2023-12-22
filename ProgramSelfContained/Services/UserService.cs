using Microsoft.EntityFrameworkCore;
using ProgramSelfContained.DataAccess;
using ProgramSelfContained.Models;

namespace ProgramSelfContained.Services;

public class UserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(User user)
    {
        var fieldNames = user.Fields.Select(x => x.Name).ToList();

        var fields = await _context.FieldDefinitions.Where(x => fieldNames.Contains(x.Name))
            .ToDictionaryAsync(x => x.Name, x => x);

        if (fieldNames.Count != fields.Count) return;

        var invalidValues = user.Fields.Where(field => !FieldService.IsFieldValueValid(fields[field.Name], field.Value))
            .ToList();

        if (invalidValues.Any())
        {
            var message = string.Join(Environment.NewLine, invalidValues);
            throw new Exception($"Those field values were invalid: {message}");
        }

        //Create in db ...
    }
}