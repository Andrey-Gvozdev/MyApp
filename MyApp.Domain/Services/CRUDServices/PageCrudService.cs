using MyApp.Contracts.Events;
using MyApp.Domain.CustomExceptions;
using MyApp.Domain.DomainModel;
using Rebus.Bus;

namespace MyApp.Domain.Services.CRUDServices;
public class PageCrudService : IPageCrudService
{
    private readonly IPageRepository pageRepository;
    private readonly IValidationCreativeNameService validationService;
    private readonly IBus bus;
    private readonly IRenderingPage renderingPage;

    public PageCrudService(IPageRepository repository, IValidationCreativeNameService validation, IBus bus, IRenderingPage renderingPage)
    {
        this.pageRepository = repository;
        this.validationService = validation;
        this.bus = bus;
        this.renderingPage = renderingPage;
    }

    public async Task<Page> Create(Page page)
    {
        await this.validationService.ValidationCreativeName(page);

        page = await this.pageRepository.Create(page);

        await this.SendRenderedPage(page);

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
        var current = await this.GetPage(id);

        await this.validationService.ValidationCreativeName(page);

        current.SetName(page.Name);
        current.SetContent(page.Content);

        await this.pageRepository.SaveChanges();

        await this.SendRenderedPage(current);

        return current;
    }

    private async Task<Page> GetPage(int id)
    {
        var page = await this.pageRepository.Get(id);
        return page ?? throw new NotFoundException("Item not found");
    }

    private async Task SendRenderedPage(Page page)
    {
        var pageRendered = await this.renderingPage.RenderingPageContent(page);
        await this.bus.Publish(new PageCreated(pageRendered.Id, pageRendered.Content));
    }
}