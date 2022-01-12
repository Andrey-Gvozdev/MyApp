namespace MyApp.Domain.DomainModel;
public interface ISnippetRepository
{
    Task<List<Snippet>> GetListAsync();

    Task<Snippet> Create(Snippet snippet);

    Task<Snippet> Get(int snippetId);

    Task SaveChanges();

    Task Delete(Snippet snippet);

    List<int> IsSnippetContains(string snippetName);
}