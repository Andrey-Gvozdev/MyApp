using Microsoft.EntityFrameworkCore;
using MyApp.CustomExceptions;
using MyApp.Domain.DomainModel;
using MyApp.Domain.Services;

namespace Infrastructure;
public class PageRepository : IPageRepository
{
    private readonly ApplicationDbContext db;
    private readonly ISnippetService snippetService;

    public PageRepository(ApplicationDbContext context, ISnippetService snippet)
    {
        this.db = context;
        this.snippetService = snippet;
    }

    public Task<List<Page>> GetListAsync()
    {
        return this.db.Pages.AsNoTracking().ToListAsync();
    }

    public async Task<Page> Create(Page page)
    {
        if (page.Content.Contains("#SNIPPET."))
        {
            page = this.snippetService.FillSnippetsList(page);
        }

        await this.db.Pages.AddAsync(page);
        await this.db.SaveChangesAsync();

        return page;
    }

    public Task<Page> Get(int id)
    {
        return this.db.Pages.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Page> Update(Page page, int id)
    {
        page.Id = id;
        var current = await this.db.Pages.FirstOrDefaultAsync(x => x.Id == page.Id);

        if (current == null)
        {
            throw new NotFoundException("Item not found");
        }

        /*if (current.Snippets?.Count > 0)
        {
            foreach (var item in current.Snippets)
            {
                page.Snippets?.Add(item);
            }
        }

        page.Snippets = current.Snippets;
        page = this.snippetService.FillSnippetsList(page);*/

        this.db.Entry(current).CurrentValues.SetValues(page);
        await this.db.SaveChangesAsync();

        return current;
    }

    public async Task Delete(Page page)
    {
        var id = page.Id;

        var newPage = new Page(page.Name, "")
        {
            Id = id,
        };

        this.db.Entry(page).CurrentValues.SetValues(newPage);

        this.db.Pages.Remove(page);
        await this.db.SaveChangesAsync();
    }
}