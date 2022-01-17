using MyApp.Domain.DomainModel;

namespace MyApp.Domain.Services;
public interface IRenderingPage
{
    Task<PageRendered> RenderingPageContent(Page page);
}
