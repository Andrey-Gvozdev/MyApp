namespace MyApp.Domain.Services;
public interface IValidationService
{
    Task ValidationNameIsUnique(string name);

    void ValidationNameIsFilled(string name);

    void ValidationNameLength(string name);

    void ValidationCreativeIsNotNull(Creative creative, int id);
}
