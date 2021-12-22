namespace MyApp.Domain.Services
{
    public interface IValidationService
    {
        Task<bool> ValidationNameIsUnique(string name);
    }
}
