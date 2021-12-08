namespace MyApp.Domain
{
    public interface ICreativeRepository
    {
        Task<IEnumerable<Creative>> GetCreativeListAsync();
        Task Post(Creative creative);
        Task<Creative> Get(int creativeId);
        Task<Creative> Patch(int creativeId, Creative creative);
        void Delete(int creativeId);
    }
}