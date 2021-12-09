using Microsoft.EntityFrameworkCore;
using MyApp.Domain.DomainModel;

namespace Infrastructure
{
    public class PageRepository : IPageRepository
    {
        private ApplicationDbContext db;

        public PageRepository(ApplicationDbContext context)
        {
            db = context;
        }

        public Task<List<Page>> GetPageListAsync()
        {
            return db.Pages.ToListAsync();
        }

        public async Task Post(Page page)
        {
            await db.Pages.AddAsync(page);
            await db.SaveChangesAsync();
        }

        public Task<Page?> Get(int pageId)
        {
            return db.Pages.FirstOrDefaultAsync(x => x.Id == pageId);
        }

        public async Task<Page> Patch(int pageId, Page page)
        {
            var current = await db.Pages.FindAsync(pageId);
            if (current == null)
                return current;

            page.Id = current.Id;

            db.Entry(current).CurrentValues.SetValues(page);
            await db.SaveChangesAsync();

            return current;
        }

        public async Task Delete(Page page)
        {
            db.Pages.Remove(page);
            await db.SaveChangesAsync();
        }
    }
}
