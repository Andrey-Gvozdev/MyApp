using Microsoft.EntityFrameworkCore;
using MyApp.Domain.DomainModel;

namespace Infrastructure;
public class PageRepository : IPageRepository
{
    private readonly ApplicationDbContext db;

    public PageRepository(ApplicationDbContext context)
    {
        this.db = context;
    }

    public Task<List<Page>> GetListAsync()
    {
        return this.db.Pages.Include(p => p.PageSnippets).AsNoTracking().ToListAsync();
    }

    public async Task<Page> Create(Page page)
    {
        await this.db.Pages.AddAsync(page);
        await this.db.SaveChangesAsync();

        return page;
    }

    public Task<Page> Get(int id)
    {
        return this.db.Pages.Include(p => p.PageSnippets).FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task SaveChanges()
    {
        return this.db.SaveChangesAsync();
    }

    public async Task Delete(Page page)
    {
        this.db.Pages.Remove(page);
        await this.db.SaveChangesAsync();
    }
}