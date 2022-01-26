using ContentApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace ContentApp.Infrastructure;
public class ContentAppDbContext : DbContext
{
    public DbSet<Page> Pages { get; set; }

    public ContentAppDbContext(DbContextOptions<ContentAppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Page>()
            .Property(b => b.PageId).ValueGeneratedNever();
    }
}