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
                var creatives = await db.Creatives.ToListAsync();
            
                return creatives;
        }

        public async Task Post(Creative creative)
        {
            db.Creatives.Add(creative);
            await db.SaveChangesAsync();
        }

        public async Task<Creative> Get(int creativeId)
        {
            return await db.Creatives.FindAsync(creativeId);
        }

        public async void Patch(Creative creative)
        {
            db.Entry(creative).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async void Delete(int creativeId)
        {
            Creative creative = await db.Creatives.FindAsync(creativeId);
            if (creative != null)
            {
                db.Creatives.Remove(creative);
                await db.SaveChangesAsync();
            }
        }
    }
}
