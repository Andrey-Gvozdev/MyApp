namespace MyApp.Domain.Services
{
    public interface IValidationService
    {
        bool ValidationNameIsUnique(string name);
    }
}
