using MyApp.Domain.CustomExceptions;
using MyApp.Domain.DomainModel;

namespace MyApp.Domain.Services.CRUDServices;
public class PageCrudService : IPageCrudService
{
    private readonly IPageRepository pageRepository;
    private readonly IValidationCreativeNameService validationService;
    private readonly ISenderRenderedPage senderRenderedPage;

    public PageCrudService(IPageRepository repository, IValidationCreativeNameService validation, ISenderRenderedPage senderRenderedPage)
    {
        this.pageRepository = repository;
        this.validationService = validation;
        this.senderRenderedPage = senderRenderedPage;
    }

    public async Task<Page> Create(Page page)
    {
        await this.validationService.ValidationCreativeName(page);

        page = await this.pageRepository.Create(page);

        await this.senderRenderedPage.SendRenderedPage(page);

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
        var pageUpdated = await this.GetPage(id);

        await this.validationService.ValidationCreativeName(page);

        pageUpdated.SetName(page.Name);
        pageUpdated.SetContent(page.Content);

        await this.pageRepository.SaveChanges();

        await this.senderRenderedPage.SendRenderedPage(pageUpdated);

        return pageUpdated;
    }

    private async Task<Page> GetPage(int id)
    {
        var page = await this.pageRepository.Get(id);

        return page ?? throw new NotFoundException("Item not found");
    }
}