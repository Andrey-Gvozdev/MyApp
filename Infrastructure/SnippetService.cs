using System.Text.RegularExpressions;
using MyApp.Domain.DomainModel;
using MyApp.Domain.Services;

namespace Infrastructure;
public class SnippetService : ISnippetService
{
    private readonly ApplicationDbContext db;

    public SnippetService(ApplicationDbContext context)
    {
        this.db = context;
    }

    public IEnumerable<string> FindSnippetNames(string content)
    {
        var start = "#SNIPPET.";
        var end = "#";

        Regex regex = new (Regex.Escape(start) + "(.*?)" + Regex.Escape(end));
        MatchCollection matches = regex.Matches(content);
        foreach (Match match in matches)
        {
            yield return match.Groups[1].Value;
        }
    }

    public List<int> SnippetValidation(IEnumerable<string> snippetNames)
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

    public async Task<Page> FillSnippetsList(Page page)
    {
        var snippets = this.SnippetValidation(this.FindSnippetNames(page.Content));

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