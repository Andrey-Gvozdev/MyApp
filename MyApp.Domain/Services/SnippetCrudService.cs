using MyApp.CustomExceptions;
using MyApp.Domain.DomainModel;

namespace MyApp.Domain.Services;
public class SnippetCrudService : ISnippetCrudService
{
    private readonly ISnippetRepository snippetRepository;
    private readonly IValidationCreativeNameService validationService;
    private readonly IIsUseSnippetValidation isUseSnippetValidation;

    public SnippetCrudService(ISnippetRepository repository, IValidationCreativeNameService validation, IIsUseSnippetValidation isUseSnippet)
    {
        this.snippetRepository = repository;
        this.validationService = validation;
        this.isUseSnippetValidation = isUseSnippet;
    }

    public async Task<Snippet> Create(Snippet snippet)
    {
        await this.validationService.ValidationCreativeName(snippet);

        return await this.snippetRepository.Create(snippet);
    }

    public async Task<Snippet> Delete(int id)
    {
        var snippet = await this.GetSnippet(id);

        this.isUseSnippetValidation.ValidationSnippet(snippet);

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

        var current = await this.GetSnippet(snippet.Id);

        if (current.Name != snippet.Name)
        {
            await this.validationService.ValidationCreativeName(snippet);
            this.isUseSnippetValidation.ValidationSnippet(snippet);
        }

        await this.snippetRepository.Update(snippet);

        return snippet;
    }

    private async Task<Snippet> GetSnippet(int id)
    {
        var snippet = await this.snippetRepository.Get(id);

        return snippet ?? throw new NotFoundException("Item not found");
    }
}
