using System.ComponentModel.DataAnnotations;
using System.Text;
using MyApp.Domain.DomainModel;
using MyApp.Domain.Services;

namespace MyApp.Services;

public class IsUseSnippetValidation : IIsUseSnippetValidation
{
    private readonly ISnippetRepository snippetRepository;

    public IsUseSnippetValidation(ISnippetRepository repository)
    {
        this.snippetRepository = repository;
    }

    public void ValidationSnippet(string snippetName)
    {
        var listIdPages = this.snippetRepository.IsSnippetContains(snippetName);
        if (listIdPages != null)
        {
            var message = new StringBuilder("This snippet is used in some pages: ", 60);

            foreach (var item in listIdPages)
            {
                message.Append(item);
                message.Append(' ');
            }

            throw new ValidationException(message.ToString());
        }
    }
}