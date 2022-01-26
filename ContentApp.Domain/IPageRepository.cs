namespace ContentApp.Domain;
public interface IPageRepository
{
    Task AddRenderedPage(int pageId, string content);

    Task<Page> Get(int pageId);

    Task SaveChangesAsync();
}
