using Microsoft.EntityFrameworkCore;
using MyApp.CustomExceptions;
using MyApp.Domain;
using MyApp.Domain.DomainModel;

namespace Infrastructure;
public class CreativeRepository : ICreativeRepository
{
    private readonly ApplicationDbContext db;

    public CreativeRepository(ApplicationDbContext context)
    {
        this.db = context;
    }

    public Task<List<Creative>> GetListAsync()
    {
        return this.db.Creatives.AsNoTracking().ToListAsync();
    }

    public async Task<Creative> Create(Creative creative)
    {
        await this.db.Creatives.AddAsync(creative);
        await this.db.SaveChangesAsync();

        return creative;
    }

    public Task<Creative> Get(int creativeId)
    {
        return this.db.Creatives.FirstOrDefaultAsync(x => x.Id == creativeId);
    }

    public async Task<Creative> Update(Creative creative)
    {
        var current = await this.Get(creative.Id);

        if (current == null)
        {
            throw new NotFoundException("Item not found");
        }

        this.db.Entry(current).CurrentValues.SetValues(creative);
        await this.db.SaveChangesAsync();

        return creative;
    }

    public async Task Delete(Creative creative)
    {
        this.db.Creatives.Remove(creative);
        await this.db.SaveChangesAsync();
    }
}