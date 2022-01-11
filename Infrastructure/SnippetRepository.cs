using Microsoft.EntityFrameworkCore;
using MyApp.CustomExceptions;
using MyApp.Domain.DomainModel;
using MyApp.Domain.Services;

namespace Infrastructure;
public class SnippetRepository : ISnippetRepository
{
    private readonly ApplicationDbContext db;

    private readonly IValidationService validationService;

    public SnippetRepository(ApplicationDbContext context, IValidationService validation)
    {
        this.db = context;
        this.validationService = validation;
    }

    public Task<List<Snippet>> GetListAsync()
    {
        return this.db.Snippets.AsNoTracking().ToListAsync();
    }

    public async Task<Snippet> Create(Snippet snippet)
    {
        await this.db.Snippets.AddAsync(snippet);
        await this.db.SaveChangesAsync();

        return snippet;
    }

    public Task<Snippet> Get(int id)
    {
        return this.db.Snippets.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Snippet> Update(Snippet snippet)
    {
        var current = await this.Get(snippet.Id);

        if (current.Name != snippet.Name)
        {
            await this.validationService.ValidationSnippet(current);
        }

        if (current == null)
        {
            throw new NotFoundException("Item not found");
        }

        this.db.Entry(current).CurrentValues.SetValues(snippet);
        await this.db.SaveChangesAsync();

        return snippet;
    }

    public async Task Delete(Snippet snippet)
    {
        this.db.Snippets.Remove(snippet);
        await this.db.SaveChangesAsync();
    }
}