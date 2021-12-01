using Microsoft.EntityFrameworkCore;
using MyProject.Models;

namespace MyApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Creative> creatives { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
