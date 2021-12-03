using Microsoft.EntityFrameworkCore;
using MyApp.Domain;

namespace Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Creative> Creatives { get; set; }
    }
}
