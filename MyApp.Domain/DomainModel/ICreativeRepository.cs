namespace MyApp.Domain
{
    public interface ICreativeRepository
    {
        Task<IEnumerable<Creative>> GetCreativeListAsync();
        Task Post(Creative creative);
        Task<Creative> Get(int creativeId);
        void Patch(Creative creative);
        void Delete(int creativeId);
    }
}