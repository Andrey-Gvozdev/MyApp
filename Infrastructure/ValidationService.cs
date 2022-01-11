using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using MyApp.Domain;
using MyApp.Domain.DomainModel;
using MyApp.Domain.Services;

namespace Infrastructure;
public class ValidationService : IValidationService
{
    private readonly ApplicationDbContext db;

    public ValidationService(ApplicationDbContext context)
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

    public async Task ValidationSnippet(Snippet snippet)
    {
        var current = await this.db.Snippets.Include(u => u.Pages).FirstOrDefaultAsync(x => x.Id == snippet.Id);
        if (current?.Pages?.Count > 0)
        {
            string pagesId = "";
            foreach (var page in current.Pages)
            {
                pagesId += page.Id + " ";
            }

            throw new ValidationException("This snippet(" + snippet.Id + ") is used in some pages: " + pagesId);
        }
    }
}