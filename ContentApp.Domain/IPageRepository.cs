namespace ContentApp.Domain;
public interface IPageRepository
{
    Task<List<Page>> GetListAsync();
}
