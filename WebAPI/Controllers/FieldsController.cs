using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.DataAccess;
using SharedLibrary.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class FieldsController : ControllerBase
{
    private readonly AppDbContext _context;

    public FieldsController()
    {
        _context = new AppDbContext();
    }

    [HttpGet]
    public async Task<IEnumerable<Field>> Get([FromQuery] FieldType? fieldType = null)
    {
        var query = _context.Fields.AsQueryable();

        if (fieldType != null) query = query.Where(x => x.Type == fieldType);

        return await query.ToListAsync();
    }
}