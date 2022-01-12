using System.Text.RegularExpressions;
using MyApp.Domain.DomainModel;
using MyApp.Domain.Services;

namespace Infrastructure;
public class PageSnippetListFillingService : IPageSnippetListFillingService
{
    private readonly ApplicationDbContext db;

    public PageSnippetListFillingService(ApplicationDbContext context)
    {
        this.db = context;
    }

    public IEnumerable<string> FindSnippetNames(string content)
    {
        Regex regex = new ("#SNIPPET.([^#]+)#");
        MatchCollection matches = regex.Matches(content);

        return matches.Select(x => x.Groups[1].Value);
    }

    public List<int> CreateSnippetIdList(IEnumerable<string> snippetNames)
    {
        var listSnippets = new List<int>();

        foreach (var item in snippetNames)
        {
            var snippet = this.db.Snippets.FirstOrDefault(x => x.Name == item);

            if (snippet != null)
            {
                listSnippets.Add(snippet.Id);
            }
        }

        return listSnippets;
    }

    public async Task<Page> FillPageSnippetList(Page page)
    {
        var snippets = this.CreateSnippetIdList(this.FindSnippetNames(page.Content));

        if (page.PageSnippets.Count > 0)
        {
            page.PageSnippets.Clear();
        }
        else
        {
            foreach (var item in snippets)
            {
                page.PageSnippets?.Add(new PageSnippet(item));
            }

            await this.db.SaveChangesAsync();
        }

        return page;
    }
}