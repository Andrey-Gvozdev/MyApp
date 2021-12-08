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

        public Creative Get(int creativeId)
        {
            return db.Creatives.Find(creativeId);
        }

        public void Patch(int creativeId, Creative creative)
        {
            var current = db.Creatives.Find(creativeId);
            if (current == null)
                return;
            
            creative.Id = current.Id;

            db.Entry(current).CurrentValues.SetValues(creative);
            db.SaveChanges();
        }

        public async void Delete(int creativeId)
        {
            Creative creative = await db.Creatives.FindAsync(creativeId);
            if (creative != null)
            {
                db.Creatives.Remove(creative);
                db.SaveChanges();
            }
        }
    }
}
