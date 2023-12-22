using Microsoft.EntityFrameworkCore;
using ProgramSelfContained.Models;

namespace ProgramSelfContained.DataAccess;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; } = null!;
    public DbSet<Field> FieldDefinitions { get; } = null!;
}