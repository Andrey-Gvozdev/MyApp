namespace MyApp.Domain
{
    public interface ICreativeRepository
    {
        Task<List<Creative>> GetCreativeListAsync();
        Task Post(Creative creative);
        Task<Creative> Get(int creativeId);
        Task<Creative> Patch(int creativeId, Creative creative);
        Task Delete(Creative creative);
    }
}