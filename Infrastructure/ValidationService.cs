using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Services;

namespace Infrastructure
{
    public class ValidationService : IValidationService
    {
        private readonly ApplicationDbContext db;

        public ValidationService(ApplicationDbContext context)
        {
            this.db = context;
        }

        public Task<bool> ValidationNameIsUnique(string name)
        {
            return this.db.Creatives.AnyAsync(x => x.Name == name);
        }
    }
}
