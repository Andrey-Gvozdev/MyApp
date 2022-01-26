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

    public async Task ValidationSnippet(string snippetName)
    {
        var listIdPages = await this.SearchPagesIdWhereUsed(snippetName);
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

    public Task<List<int>> SearchPagesIdWhereUsed(string snippetName)
    {
        return this.snippetRepository.GetRelatedPageIds(snippetName);
    }
}