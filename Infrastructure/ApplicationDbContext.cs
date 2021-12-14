using Microsoft.EntityFrameworkCore;
using MyApp.Domain;
using MyApp.Domain.DomainModel;
using MyApp.Domain.Services;

namespace Infrastructure;

public class ApplicationDbContext : DbContext
{
    public DbSet<Creative> Creatives { get; set; }

    public DbSet<Page> Pages { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        // Database.EnsureDeleted();
        // Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CreativeConfiguration).Assembly);
        modelBuilder.Entity<Creative>()
            .HasDiscriminator()
            .HasValue<Creative>("Creative")
            .HasValue<Page>("Page");
    }
}
