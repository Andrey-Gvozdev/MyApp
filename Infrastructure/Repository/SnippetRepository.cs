using Microsoft.EntityFrameworkCore;
using MyApp.Domain.DomainModel;

namespace Infrastructure.Repository;
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

    public Task<List<int>> GetRelatedPageIds(string snippetName)
    {
        return this.db.Pages.Where(x => x.PageSnippets.Any(x => x.SnippetName == snippetName)).Select(x => x.Id).ToListAsync();
    }

    public Task<Snippet> Get(string snippetName)
    {
        return this.db.Snippets.FirstOrDefaultAsync(x => x.Name == snippetName);
    }

    public Task<List<int>> SearchPagesIdWhereUsed(string snippetName)
    {
        return this.GetRelatedPageIds(snippetName);
    }
}