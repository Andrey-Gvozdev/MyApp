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

    public async Task<List<Page>> GetListAsync()
    {
        return await this.db.Pages.AsNoTracking().ToListAsync();
    }

    public async Task AddRenderedPage(int pageId, string content)
    {
        await this.db.Pages.AddAsync(new Page(pageId, content));
        await this.db.SaveChangesAsync();
    }
}
