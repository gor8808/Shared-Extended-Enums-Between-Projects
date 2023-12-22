using Microsoft.EntityFrameworkCore;
using SharedLibrary.Convertors;
using SharedLibrary.Models;

namespace SharedLibrary.DataAccess;

public class AppDbContext : DbContext
{
    public DbSet<Field> Fields { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=EntitiesDataDB;Username=postgres;Password=fs86jj0b");

        optionsBuilder.LogTo(Console.WriteLine);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Field>().Property(x => x.Id).ValueGeneratedOnAdd();

        modelBuilder.Entity<Field>()
            .Property(x => x.Type)
            .HasConversion<FieldTypeValueConvertor>();

        base.OnModelCreating(modelBuilder);
    }
}