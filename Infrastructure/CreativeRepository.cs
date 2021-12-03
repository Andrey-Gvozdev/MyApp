using Microsoft.EntityFrameworkCore;
using MyApp.Domain;

namespace Infrastructure
{
    internal class CreativeRepository : ICreativeRepository
    {
        private ApplicationDbContext db;

        public CreativeRepository(ApplicationDbContext context)
        {
            db = context;
        }

        public void Post(Creative creative)
        {
            db.Creatives.Add(creative);
            db.SaveChanges();
        }

        public Creative Get(int creativeId)
        {
            return db.Creatives.Find(creativeId);
        }

        public void Patch(Creative creative)
        {
            db.Entry(creative).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int creativeId)
        {
            Creative creative = db.Creatives.Find(creativeId);
            if (creative != null)
            {
                db.Creatives.Remove(creative);
                db.SaveChanges();
            }
        }
    }
}
