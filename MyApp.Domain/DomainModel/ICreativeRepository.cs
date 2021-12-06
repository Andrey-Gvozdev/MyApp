namespace MyApp.Domain
{
    public interface ICreativeRepository
    {
        void Post(Creative creative);
        Creative Get(int creativeId);
        void Patch(Creative creative);
        void Delete(int creativeId);
    }
}