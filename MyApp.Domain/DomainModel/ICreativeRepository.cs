namespace MyApp.Domain.DomainModel;
public interface ICreativeRepository
{
    Task<bool> CreativeNameIsUnique(Creative creative);
}
