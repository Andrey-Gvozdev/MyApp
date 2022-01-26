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

    public async Task Handle(PageRendered message)
    {
        await this.HandleHelper(message);

        await Task.CompletedTask;
    }

    private async Task HandleHelper(PageRendered message)
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
