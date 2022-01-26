using MyApp.Domain.CustomExceptions;
using MyApp.Domain.DomainModel;

namespace MyApp.Domain.Services.CRUDServices;
public class SnippetCrudService : ISnippetCrudService
{
    private readonly ISnippetRepository snippetRepository;
    private readonly IValidationCreativeNameService validationService;
    private readonly IIsUseSnippetValidation isUseSnippetValidation;
    private readonly ISenderRenderedPage senderRenderedPage;

    public SnippetCrudService(ISnippetRepository repository, IValidationCreativeNameService validation, IIsUseSnippetValidation isUseSnippet, ISenderRenderedPage senderRenderedPage)
    {
        this.snippetRepository = repository;
        this.validationService = validation;
        this.isUseSnippetValidation = isUseSnippet;
        this.senderRenderedPage = senderRenderedPage;
    }

    public async Task<Snippet> Create(Snippet snippet)
    {
        await this.validationService.ValidationCreativeName(snippet);

        snippet = await this.snippetRepository.Create(snippet);

        await this.senderRenderedPage.SendRenderedPages(await this.snippetRepository.SearchPagesIdWhereUsed(snippet.Name));

        return snippet;
    }

    public async Task<Snippet> Delete(int id)
    {
        var snippet = await this.GetSnippet(id);

        await this.isUseSnippetValidation.ValidationSnippet(snippet.Name);

        await this.snippetRepository.Delete(snippet);

        return snippet;
    }

    public Task<Snippet> GetById(int id)
    {
        return this.GetSnippet(id);
    }

    public async Task<Snippet> Update(int id, string content)
    {
        var snippet = await this.GetSnippet(id);

        snippet.SetContent(content);

        await this.snippetRepository.SaveChanges();

        await this.senderRenderedPage.SendRenderedPages(await this.snippetRepository.SearchPagesIdWhereUsed(snippet.Name));

        return snippet;
    }

    private async Task<Snippet> GetSnippet(int id)
    {
        var snippet = await this.snippetRepository.Get(id);

        return snippet ?? throw new NotFoundException("Item not found");
    }
}
