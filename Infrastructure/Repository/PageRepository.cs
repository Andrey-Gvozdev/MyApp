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

    public async Task<Page> Update(Page page)
    {
        var current = await this.Get(page.Id);

        current.SetName(page.Name);
        current.SetContent(page.Content);
        current.SetPageSnippetList(page.PageSnippets);

        await this.db.SaveChangesAsync();

        return current;
    }

    public async Task Delete(Page page)
    {
        this.db.Pages.Remove(page);
        await this.db.SaveChangesAsync();
    }
}