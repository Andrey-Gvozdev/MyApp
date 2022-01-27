using MyApp.Domain.DomainModel;
using MyApp.Domain.Services;
using Rebus.Bus;

namespace MyApp.Services;
public class SenderRenderedPage : ISenderRenderedPage
{
    private readonly IBus bus;
    private readonly IRenderingPage renderingPage;
    private readonly IPageRepository pageRepository;

    public SenderRenderedPage(IBus bus, IRenderingPage renderingPage, IPageRepository pageRepository)
    {
        this.bus = bus;
        this.renderingPage = renderingPage;
        this.pageRepository = pageRepository;
    }

    public async Task SendRenderedPage(Page page)
    {
        await this.bus.Publish(await this.renderingPage.RenderingPageContent(page));
    }

    public async Task SendRenderedPages(List<int> pagesId)
    {
        var pagesList = await this.SearchPagesForRenering(pagesId);

        foreach (var page in pagesList)
        {
            await this.SendRenderedPage(page);
        }
    }

    private async Task<List<Page>> SearchPagesForRenering(List<int> pagesId)
    {
        List<Page> pagesList = new ();

        foreach (var pageId in pagesId)
        {
            pagesList.Add(await this.pageRepository.Get(pageId));
        }

        return pagesList;
    }
}
