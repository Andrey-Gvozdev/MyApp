namespace MyApp.Domain.Services;
public interface ICreativeCrudService
{
    Task<Creative> Create(Creative creative);

    Task<Creative> GetById(int id);

    Task<Creative> Delete(int id);

    Task<Creative> Update(int id, Creative creative);
}
