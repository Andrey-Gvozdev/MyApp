using Microsoft.EntityFrameworkCore;
using MyApp.Domain.DomainModel;

namespace Infrastructure.Repository;
public class CreativeRepository : ICreativeRepository
{
    private readonly ApplicationDbContext db;

    public CreativeRepository(ApplicationDbContext context)
    {
        this.db = context;
    }

    public Task<bool> CreativeNameIsUnique(Creative creative)
    {
        return this.db.Creatives.AnyAsync(x => x.Name == creative.Name && x.Id != creative.Id);
    }
}
