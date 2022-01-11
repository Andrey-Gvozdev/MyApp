using MyApp.Domain.DomainModel;

namespace MyApp.Domain.Services;
public interface IValidationService
{
    Task ValidationCreativeName(Creative creative);

    Task ValidationSnippetDeleting(Snippet snippet);
}
