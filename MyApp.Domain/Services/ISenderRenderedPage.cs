using MyApp.Domain.DomainModel;

namespace MyApp.Domain.Services;
public interface ISenderRenderedPage
{
    Task SendRenderedPage(Page page);

    Task SendRenderedPages(List<int> pagesId);
}
