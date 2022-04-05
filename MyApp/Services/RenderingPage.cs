using System.Text.RegularExpressions;
using MyApp.Contracts.Events;
using MyApp.Domain.DomainModel;
using MyApp.Domain.Services;

namespace MyApp.Services;
public class RenderingPage : IRenderingPage
{
    private readonly ISnippetRepository snippetRepository;

    public RenderingPage(ISnippetRepository snippetRep)
    {
        this.snippetRepository = snippetRep;
    }

    public async Task<PageRendered> RenderingPageContent(Page page)
    {
        var renderedContent = page.Content;

        foreach (var item in page.PageSnippets)
        {
            var pattern = $"#SNIPPET.{item.SnippetName}#";
            var snippet = await this.snippetRepository.Get(item.SnippetName);

            if (snippet != null)
            {
                renderedContent = Regex.Replace(renderedContent, pattern, snippet.Content);
            }
            else
            {
                renderedContent = Regex.Replace(renderedContent, pattern, string.Empty);
            }
        }

        return new PageRendered(page.Id, renderedContent);
    }
}
