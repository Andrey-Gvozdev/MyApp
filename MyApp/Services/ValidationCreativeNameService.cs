using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using MyApp.Domain;
using MyApp.Domain.Services;

namespace Infrastructure;
public class ValidationCreativeNameService : IValidationCreativeNameService
{
    private readonly ApplicationDbContext db;

    public ValidationCreativeNameService(ApplicationDbContext context)
    {
        this.db = context;
    }

    public async Task ValidationCreativeName(Creative creative)
    {
        if (string.IsNullOrWhiteSpace(creative.Name))
        {
            throw new ValidationException("Name field is empty");
        }

        if (await this.db.Creatives.AnyAsync(x => x.Name == creative.Name && x.Id != creative.Id))
        {
            throw new ValidationException("This name is already taken");
        }

        if (creative.Name.Length > 30)
        {
            throw new ValidationException("Name field must be less than 30 characters");
        }
    }
}