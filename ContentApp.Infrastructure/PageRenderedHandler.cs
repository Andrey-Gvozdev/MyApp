using ContentApp.Domain;
using MyApp.Contracts.Events;
using Rebus.Handlers;

namespace ContentApp.Infrastructure;
public class PageRenderedHandler : IHandleMessages<PageRendered>
{
    private readonly IPageRepository pageRepository;

    public PageRenderedHandler(IPageRepository pageRepository)
    {
        this.pageRepository = pageRepository;
    }

    public Task Handle(PageRendered message)
    {
        return this.CreateOrUpdatePage(message);
    }

    private async Task CreateOrUpdatePage(PageRendered message)
    {
        var page = await this.pageRepository.Get(message.PageId);

        if (page != null)
        {
            page.SetContent(message.Content);
            await this.pageRepository.SaveChangesAsync();
        }
        else
        {
            await this.pageRepository.AddRenderedPage(message.PageId, message.Content);
        }
    }
}
