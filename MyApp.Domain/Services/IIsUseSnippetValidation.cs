using MyApp.Domain.DomainModel;

namespace MyApp.Domain.Services;

public interface IIsUseSnippetValidation
{
    void ValidationSnippet(Snippet snippet);
}