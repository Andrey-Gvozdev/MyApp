namespace MyApp.Domain.Services;
public interface IValidationService
{
    Task ValidationCreativeName(string name);

    void ValidationCreativeIsNotNull(Creative creative, int id);
}
