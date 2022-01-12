using MyApp.Domain.DomainModel;

namespace MyApp.Domain.Services;

public interface IDeleteSnippetValidation
{
    void ValidationSnippet(Snippet snippet);
}