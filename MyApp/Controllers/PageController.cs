using Microsoft.AspNetCore.Mvc;
using MyApp.Domain.DomainModel;
using MyApp.Domain.Services;
using MyApp.Domain.Services.CRUDServices;
using Rebus.Bus;

namespace MyApp.Controllers;

[ApiController]
[Route("api/page")]
[Produces("application/json")]
public class PageController : Controller
{
    private readonly IPageRepository pageRepository;
    private readonly IPageCrudService pageCrudService;
    private readonly IBus bus;
    private readonly IRenderingPage renderingPage;

    public PageController(IPageRepository pageRepository, IPageCrudService pageCrud, IBus bus, IRenderingPage renderingPage)
    {
        this.pageRepository = pageRepository;
        this.pageCrudService = pageCrud;
        this.bus = bus;
        this.renderingPage = renderingPage;
    }

    [HttpGet]
    public Task<List<Page>> Get()
    {
        return this.pageRepository.GetListAsync();
    }

    [HttpPost]
    public async Task<Page> Post(Page page)
    {
        var pageCreated = await this.pageCrudService.Create(page);
        var kek = await this.renderingPage.RenderingPageContent(pageCreated);
        await this.bus.Send(kek);
        return pageCreated;
    }

    [HttpGet("{pageId}")]
    public Task<Page> Get(int pageId)
    {
        return this.pageCrudService.GetById(pageId);
    }

    [HttpDelete]
    public Task<Page> Delete(int pageId)
    {
        return this.pageCrudService.Delete(pageId);
    }

    [HttpPut]
    public Task<Page> Put(int pageId, Page page)
    {
        return this.pageCrudService.Update(pageId, page);
    }
}
