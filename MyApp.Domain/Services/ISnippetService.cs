using MyApp.Domain.DomainModel;

namespace MyApp.Domain.Services;
public interface ISnippetService
{
    IEnumerable<string> FindSnippetNames(string content);

    List<Snippet> SnippetValidation(IEnumerable<string> snippetNames);

    Page FillSnippetsList(Page page);
}
