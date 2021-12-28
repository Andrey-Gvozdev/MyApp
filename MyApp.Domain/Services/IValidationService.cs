namespace MyApp.Domain.Services;
public interface IValidationService
{
    Task ValidationCreativeName(Creative creative);

    void ValidationCreativeIsNotNull(Creative creative, int id);
}
