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

    public List<Snippet> SnippetValidation(IEnumerable<string> snippetNames)
    {
        var listSnippets = new List<Snippet>();

        foreach (var item in snippetNames)
        {
            var snippet = this.db.Snippets.FirstOrDefault(x => x.Name == item);

            if (snippet != null)
            {
                listSnippets.Add(snippet);
            }
        }

        return listSnippets;
    }

    public Page FillSnippetsList(Page page)
    {
        var snippets = this.SnippetValidation(this.FindSnippetNames(page.Content));

        if (snippets.Count != 0)
        {
            foreach (var item in snippets)
            {
                if (!page.Snippets.Contains(item))
                {
                    page.Snippets.Add(item);
                }
            }

            foreach (var item in page.Snippets)
            {
                if (!snippets.Contains(item))
                {
                    page.Snippets.Remove(item);
                }
            }
        }
        else
        {
            page.Snippets.Clear();
        }

        return page;
    }
}