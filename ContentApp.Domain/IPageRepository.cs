namespace ContentApp.Domain;
public interface IPageRepository
{
    Task<List<Page>> GetListAsync();

    Task AddRenderedPage(int pageId, string content);
}
