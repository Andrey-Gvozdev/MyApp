using Microsoft.EntityFrameworkCore;
using MyApp.Domain;
using MyApp.Domain.DomainModel;

namespace Infrastructure;
public class PageRepository : IPageRepository
{
    private readonly ApplicationDbContext db;

    public PageRepository(ApplicationDbContext context)
    {
        this.db = context;
    }

    public Task<List<Creative>> GetListAsync()
    {
        return this.db.Pages.OfType<Creative>().ToListAsync();
    }

    public async Task<Creative> Post(Creative creative)
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

    public Task<Creative> Get(int pageId)
    {
        return this.db.Pages.OfType<Creative>().FirstOrDefaultAsync(x => x.Id == pageId);
    }

    public async Task<Creative> Patch(Creative oldPage, Creative? page)
    {
        if (page == null || page.Name == null)
        {
            return page;
        }
        else
        {
            page.Id = oldPage.Id;

            this.db.Entry(oldPage).CurrentValues.SetValues(page);
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
    }

    public async Task Delete(Creative creative)
    {
        Page page = (Page)creative;
        this.db.Pages.Remove(page);
        await this.db.SaveChangesAsync();
    }
}