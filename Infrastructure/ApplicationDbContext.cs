using Microsoft.EntityFrameworkCore;
using MyApp.Domain;
using MyApp.Domain.DomainModel;
using MyApp.Domain.Services;

namespace Infrastructure;
public class ApplicationDbContext : DbContext
{
    public DbSet<Creative> Creatives { get; set; }

    public DbSet<Page> Pages { get; set; }

    public DbSet<Snippet> Snippets { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CreativeConfiguration).Assembly);
        modelBuilder.Entity<Page>()
                .HasMany(c => c.Snippets)
                .WithMany(s => s.Pages)
                .UsingEntity(j => j.ToTable("PageSnippet"));

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}