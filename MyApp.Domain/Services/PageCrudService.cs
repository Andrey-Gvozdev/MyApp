using MyApp.CustomExceptions;
using MyApp.Domain.DomainModel;

namespace MyApp.Domain.Services;
public class PageCrudService : IPageCrudService
{
    private readonly IPageRepository pageRepository;
    private readonly IValidationService validationService;
    private readonly IPageSnippetListFillingService snippetService;

    public PageCrudService(IPageRepository repository, IValidationService validation, IPageSnippetListFillingService snippet)
    {
        this.pageRepository = repository;
        this.validationService = validation;
        this.snippetService = snippet;
    }

    public async Task<Page> Create(Page page)
    {
        await this.validationService.ValidationCreativeName(page);

        if (page.Content.Contains("#SNIPPET."))
        {
            page = await this.snippetService.FillPageSnippetList(page);
        }

        page = await this.pageRepository.Create(page);

        return page;
    }

    public async Task<Page> GetById(int id)
    {
        var page = await this.GetPage(id);

        return page;
    }

    public async Task<Page> Delete(int id)
    {
        var page = await this.GetPage(id);

        await this.pageRepository.Delete(page);

        return page;
    }

    public async Task<Page> Update(int id, Page page)
    {
        page.Id = id;

        page = await this.snippetService.FillPageSnippetList(page);
        await this.validationService.ValidationCreativeName(page);

        await this.pageRepository.Update(page);

        return page;
    }

    private async Task<Page> GetPage(int id)
    {
        var page = await this.pageRepository.Get(id);
        return page ?? throw new NotFoundException("Item not found");
    }
}