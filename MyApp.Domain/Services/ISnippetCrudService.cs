using MyApp.Domain.DomainModel;

namespace MyApp.Domain.Services;
public interface ISnippetCrudService
{
    Task<Snippet> Create(Snippet snippet);

    Task<Snippet> GetById(int id);

    Task<Snippet> Delete(int id);

    Task<Snippet> Update(int id, string content);
}
