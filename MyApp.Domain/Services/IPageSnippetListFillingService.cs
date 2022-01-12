using MyApp.Domain.DomainModel;

namespace MyApp.Domain.Services;
public interface IPageSnippetListFillingService
{
    IEnumerable<string> FindSnippetNames(string content);

    List<int> CreateSnippetIdList(IEnumerable<string> snippetNames);

    Task<Page> FillPageSnippetList(Page page);
}