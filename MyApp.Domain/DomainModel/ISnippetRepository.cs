namespace MyApp.Domain.DomainModel;
public interface ISnippetRepository
{
    Task<List<Snippet>> GetListAsync();

    Task<Snippet> Create(Snippet snippet);

    Task<Snippet> Get(int snippetId);

    Task<Snippet> Get(string snippetName);

    Task SaveChanges();

    Task Delete(Snippet snippet);

    Task<List<int>> GetRelatedPageIds(string snippetName);

    Task<List<int>> SearchPagesIdWhereUsed(string snippetName);
}