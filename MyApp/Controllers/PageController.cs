using Microsoft.AspNetCore.Mvc;
using MyApp.Domain.DomainModel;
using MyApp.Domain.Services;

namespace MyApp.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
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
    public Task<List<Page>> GetList()
    {
        return this.pageRepository.GetListAsync();
    }

    [HttpPost]
    public Task<Page> Create(Page page)
    {
        return this.pageCrudService.Create(page);
    }

    [HttpGet]
    public Task<Page> GetById(int pageId)
    {
        return this.pageCrudService.GetById(pageId);
    }

    [HttpDelete]
    public Task<Page> Delete(int pageId)
    {
        return this.pageCrudService.Delete(pageId);
    }

    [HttpPut]
    public Task<Page> Update(int pageId, Page page)
    {
        return this.pageCrudService.Update(pageId, page);
    }
}
