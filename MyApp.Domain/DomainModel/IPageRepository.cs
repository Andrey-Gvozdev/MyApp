namespace MyApp.Domain.DomainModel;
public interface IPageRepository
{
    Task<List<Page>> GetListAsync();

    Task<Page> Create(Page page);

    Task<Page> Get(int id);

    Task<Page> Update(Page page, int id);

    Task Delete(Page page);
}
