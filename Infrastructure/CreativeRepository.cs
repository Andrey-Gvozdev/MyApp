using Microsoft.EntityFrameworkCore;
using MyApp.Domain;

namespace Infrastructure
{
    public class CreativeRepository : ICreativeRepository
    {
        private ApplicationDbContext db;

        public CreativeRepository(ApplicationDbContext context)
        {
            db = context;
        }

        public async Task<IEnumerable<Creative>> GetCreativeListAsync()
        {
            return await db.Creatives.ToListAsync();
        }

        public async Task Post(Creative creative)
        {
            await db.Creatives.AddAsync(creative);
            await db.SaveChangesAsync();
        }

        public async Task<Creative> Get(int creativeId)
        {
            return await db.Creatives.FindAsync(creativeId);
        }

        public async Task<Creative> Patch(int creativeId, Creative creative)
        {
            var current = await db.Creatives.FindAsync(creativeId);
            if (current == null)
                return current;
            
            creative.Id = current.Id;

            db.Entry(current).CurrentValues.SetValues(creative);
            await db.SaveChangesAsync();

            return current;
        }

        public async Task Delete(Creative creative)
        {
            db.Creatives.Remove(creative);
            await db.SaveChangesAsync();
        }
    }
}
