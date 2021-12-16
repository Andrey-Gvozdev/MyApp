namespace MyApp.Domain.DomainModel;
public interface ICreativeRepository
{
    Task<List<Creative>> GetListAsync();

    Task<Creative> Post(Creative creative);

    Task<Creative> Get(int creativeId);

    Task<Creative> Patch(Creative oldCreative, Creative creative);

    Task Delete(Creative creative);
}
