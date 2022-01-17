using System.Text.RegularExpressions;
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

            renderedContent = Regex.Replace(renderedContent, pattern, snippet.Content);
        }

        return new PageRendered(page.Id, renderedContent);
    }
}
