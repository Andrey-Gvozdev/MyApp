namespace MyApp.Domain.Services;
public interface IValidationService
{
    Task<bool> ValidationNameIsUnique(string name);

    void ValidationNameIsFilled(string name);

    void ValidationNameLength(string name);
}
