using MyApp.Domain.DomainModel;

namespace MyApp.Domain.Services.CRUDServices;
public interface IPageCrudService
{
    Task<Page> Create(Page page);

    Task<Page> GetById(int id);

    Task<Page> Delete(int id);

    Task<Page> Update(int id, Page page);
}
