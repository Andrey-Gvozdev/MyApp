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
        return this.db.Pages.OfType<Creative>().ToListAsync();
    }

    public async Task<Creative> Create(Creative creative)
    {
        Page? page = (Page)creative;
        if (page.Name == null)
        {
            return page;
        }

        await this.db.Pages.AddAsync(page);

        try
        {
            await this.db.SaveChangesAsync();
            return page;
        }
        catch
        {
            page = null;
            return page;
        }
    }

    public Task<Creative> Get(int creativeId)
    {
        return this.db.Pages.OfType<Creative>().FirstOrDefaultAsync(x => x.Id == creativeId);
    }

    public async Task<Creative> Update(Creative oldPage, Creative? creative)
    {
        if (creative == null || creative.Name == null)
        {
            return creative;
        }
        else
        {
            creative.Id = oldPage.Id;

            this.db.Entry(oldPage).CurrentValues.SetValues(creative);
            try
            {
                await this.db.SaveChangesAsync();
                return creative;
            }
            catch
            {
                creative = null;
                return creative;
            }
        }
    }

    public async Task Delete(Creative creative)
    {
        Page page = (Page)creative;
        this.db.Pages.Remove(page);
        await this.db.SaveChangesAsync();
    }
}