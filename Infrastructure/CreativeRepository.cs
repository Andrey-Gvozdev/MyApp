using Microsoft.EntityFrameworkCore;
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
        return this.db.Pages.OfType<Creative>().AsNoTracking().ToListAsync();
    }

    public async Task<Creative> Create(Creative creative)
    {
        Page? page = (Page)creative;

        await this.db.Pages.AddAsync(page);
        await this.db.SaveChangesAsync();

        return page;
    }

    public Task<Creative> Get(int creativeId)
    {
        return this.db.Pages.OfType<Creative>().FirstOrDefaultAsync(x => x.Id == creativeId);
    }

    public async Task<Creative> Update(Creative oldPage, Creative creative)
    {
        creative.Id = oldPage.Id;

        this.db.Entry(oldPage).CurrentValues.SetValues(creative);
        await this.db.SaveChangesAsync();

        return creative;
    }

    public async Task Delete(Creative creative)
    {
        Page page = (Page)creative;
        this.db.Pages.Remove(page);
        await this.db.SaveChangesAsync();
    }
}