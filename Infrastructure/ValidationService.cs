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

        public bool ValidationNameIsUnique(string name)
        {
            var validateName = this.db.Creatives.FirstOrDefault(x => x.Name == name);

            if (validateName != null)
            {
                return false;
            }

            return true;
        }
    }
}
