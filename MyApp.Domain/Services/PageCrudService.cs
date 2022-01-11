using MyApp.CustomExceptions;
using MyApp.Domain.DomainModel;

namespace MyApp.Domain.Services;
public class PageCrudService : IPageCrudService
{
    private readonly IPageRepository pageRepository;
    private readonly IValidationService validationService;

    public PageCrudService(IPageRepository repository, IValidationService validation)
    {
        this.pageRepository = repository;
        this.validationService = validation;
    }

    public async Task<Page> Create(Page page)
    {
        await this.validationService.ValidationCreativeName(page);

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
        await this.validationService.ValidationCreativeName(page);
        await this.pageRepository.Update(page, id);

        return page;
    }

    private async Task<Page> GetPage(int id)
    {
        var page = await this.pageRepository.Get(id);
        return page ?? throw new NotFoundException("Item not found");
    }
}