namespace MyApp.Domain
{
    public interface ICreativeRepository
    {
        Task<IEnumerable<Creative>> GetCreativeListAsync();
        Task Post(Creative creative);
        Task<Creative> Get(int creativeId);
        void Patch(int creativeId, Creative creative);
        void Delete(int creativeId);
    }
}