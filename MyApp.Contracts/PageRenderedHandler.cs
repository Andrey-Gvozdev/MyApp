using ContentApp.Domain;
using MyApp.Contracts.Events;
using Rebus.Handlers;

namespace MyApp.Contracts;
public class PageRenderedHandler : IHandleMessages<PageRendered>
{
    private readonly IPageRepository pageRepository;

    public PageRenderedHandler(IPageRepository pageRepository)
    {
        this.pageRepository = pageRepository;
    }

    public async Task Handle(PageRendered message)
    {
        await this.pageRepository.AddRenderedPage(message.PageId, message.Content);
        await Task.CompletedTask;
    }
}
