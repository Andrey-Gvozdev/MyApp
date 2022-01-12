using Microsoft.EntityFrameworkCore;
using MyApp.Domain.DomainModel;
using MyApp.Domain.Services;

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

    public async Task<Snippet> Update(Snippet snippet)
    {
        var current = await this.Get(snippet.Id);

        current.SetName(snippet.Name);
        current.SetContent(snippet.Content);

        await this.db.SaveChangesAsync();

        return snippet;
    }

    public async Task Delete(Snippet snippet)
    {
        this.db.Snippets.Remove(snippet);
        await this.db.SaveChangesAsync();
    }
}