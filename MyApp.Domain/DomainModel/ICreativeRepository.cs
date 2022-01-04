namespace MyApp.Domain.DomainModel;
public interface ICreativeRepository
{
    Task<List<Creative>> GetListAsync();

    Task<Creative> Create(Creative creative);

    Task<Creative> Get(int creativeId);

    Task<Creative> Update(Creative creative);

    Task Delete(Creative creative);
}
