using MyApp.Domain.DomainModel;

namespace MyApp.Domain.Services;
public interface ISnippetService
{
    IEnumerable<string> FindSnippetNames(string content);

    List<int> SnippetValidation(IEnumerable<string> snippetNames);

    Page FillSnippetsList(Page page);
}