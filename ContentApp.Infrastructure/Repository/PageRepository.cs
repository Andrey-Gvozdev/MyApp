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

    public Task<List<Page>> GetListAsync()
    {
        return this.db.Pages.AsNoTracking().ToListAsync();
    }
}
