using Microsoft.AspNetCore.Mvc;
using MyApp.Domain.DomainModel;
using MyApp.Domain.Services;

namespace MyApp.Controllers;

[ApiController]
[Route("api/page")]
[Produces("application/json")]
public class PageController : Controller
{
    private readonly IPageRepository pageRepository;
    private readonly IPageCrudService pageCrudService;

    public PageController(IPageRepository pageRepository, IPageCrudService pageCrud)
    {
        this.pageRepository = pageRepository;
        this.pageCrudService = pageCrud;
    }

    [HttpGet]
    public Task<List<Page>> Get()
    {
        return this.pageRepository.GetListAsync();
    }

    [HttpPost]
    public Task<Page> Post(Page page)
    {
        return this.pageCrudService.Create(page);
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
