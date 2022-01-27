using ContentApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace ContentApp.Infrastructure.Repository;
public class PageRepository : IPageRepository
{
    private readonly ContentAppDbContext db;

    public PageRepository(ContentAppDbContext context)
    {
        this.db = context;
    }

    public async Task AddRenderedPage(int pageId, string content)
    {
        await this.db.Pages.AddAsync(new Page(pageId, content));
        await this.SaveChangesAsync();
    }

    public Task<Page> Get(int pageId)
    {
        return this.db.Pages.FirstOrDefaultAsync(x => x.PageId == pageId);
    }

    public Task SaveChangesAsync()
    {
        return this.db.SaveChangesAsync();
    }
}
