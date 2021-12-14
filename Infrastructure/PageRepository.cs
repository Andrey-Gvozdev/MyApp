namespace Infrastructure;
using Microsoft.EntityFrameworkCore;
using MyApp.Domain;
using MyApp.Domain.DomainModel;

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

    public async Task Post(Creative creative)
    {
        Page page = (Page)creative;
        await this.db.Pages.AddAsync(page);
        await this.db.SaveChangesAsync();
    }

    public Task<Creative> Get(int pageId)
    {
        return this.db.Pages.OfType<Creative>().FirstOrDefaultAsync(x => x.Id == pageId);
    }

    public async Task<Creative> Patch(int pageId, Creative page)
    {
        var current = await this.db.Pages.FindAsync(pageId);
        if (current == null)
        {
            return current;
        }

        page.Id = current.Id;

        this.db.Entry(current).CurrentValues.SetValues(page);
        await this.db.SaveChangesAsync();

        return current;
    }

    public async Task Delete(Creative creative)
    {
        Page page = (Page)creative;
        this.db.Pages.Remove(page);
        await this.db.SaveChangesAsync();
    }
}