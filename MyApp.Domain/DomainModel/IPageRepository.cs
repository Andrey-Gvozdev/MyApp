namespace MyApp.Domain.DomainModel
{
    public interface IPageRepository
    {
        Task<List<Page>> GetPageListAsync();
        Task Post(Page page);
        Task<Page> Get(int pageId);
        Task<Page> Patch(int pageId, Page page);
        Task Delete(Page page);
    }
}
