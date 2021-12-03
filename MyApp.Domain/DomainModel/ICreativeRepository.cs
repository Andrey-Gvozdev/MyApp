namespace MyApp.Domain
{
    public class ICreativeRepository
    {
        void Post(Creative creative) { }
        void Get(int? creativeId) { }
        void Patch(Creative creative) { }
        void Delete(int? creativeId) { }
    }
}