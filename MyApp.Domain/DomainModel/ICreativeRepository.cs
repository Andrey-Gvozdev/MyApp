namespace MyApp.Domain
{
    public interface ICreativeRepository
    {
        IEnumerable<Creative> GetCreativeList();
        void Post(Creative creative);
        Creative Get(int creativeId);
        void Patch(Creative creative);
        void Delete(int creativeId);
    }
}