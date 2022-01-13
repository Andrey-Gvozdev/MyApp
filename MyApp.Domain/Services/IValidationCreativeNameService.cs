using MyApp.Domain.DomainModel;

namespace MyApp.Domain.Services;
public interface IValidationCreativeNameService
{
    Task ValidationCreativeName(Creative creative);
}
