namespace MyApp.Domain.Services;

public interface IIsUseSnippetValidation
{
    Task ValidationSnippet(string snippetName);
}