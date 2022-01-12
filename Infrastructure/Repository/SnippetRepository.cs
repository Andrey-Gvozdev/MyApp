using Microsoft.EntityFrameworkCore;
using MyApp.Domain.DomainModel;

namespace Infrastructure;
public class SnippetRepository : ISnippetRepository
{
    private readonly ApplicationDbContext db;

    public SnippetRepository(ApplicationDbContext context)
    {
        this.db = context;
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

    public Task SaveChanges()
    {
        return this.db.SaveChangesAsync();
    }

    public async Task Delete(Snippet snippet)
    {
        this.db.Snippets.Remove(snippet);
        await this.db.SaveChangesAsync();
    }

    public List<int> IsSnippetContains(string snippetName)
    {
        return this.db.Pages.Where(x => x.PageSnippets.Any(x => x.SnippetName == snippetName)).Select(x => x.Id).ToList();
    }
}