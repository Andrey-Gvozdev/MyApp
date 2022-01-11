using MyApp.CustomExceptions;
using MyApp.Domain.DomainModel;

namespace MyApp.Domain.Services;
public class SnippetCrudService : ISnippetCrudService
{
    private readonly ISnippetRepository snippetRepository;
    private readonly IValidationService validationService;

    public SnippetCrudService(ISnippetRepository repository, IValidationService validation)
    {
        this.snippetRepository = repository;
        this.validationService = validation;
    }

    public async Task<Snippet> Create(Snippet snippet)
    {
        await this.validationService.ValidationCreativeName(snippet);

        return await this.snippetRepository.Create(snippet);
    }

    public async Task<Snippet> Delete(int id)
    {
        var snippet = await this.GetSnippet(id);

        await this.validationService.ValidationSnippet(snippet);
        await this.snippetRepository.Delete(snippet);

        return snippet;
    }

    public Task<Snippet> GetById(int id)
    {
        return this.GetSnippet(id);
    }

    public async Task<Snippet> Update(int id, Snippet snippet)
    {
        snippet.Id = id;

        await this.validationService.ValidationCreativeName(snippet);
        await this.snippetRepository.Update(snippet);

        return snippet;
    }

    private async Task<Snippet> GetSnippet(int id)
    {
        var snippet = await this.snippetRepository.Get(id);

        return snippet ?? throw new NotFoundException("Item not found");
    }
}
