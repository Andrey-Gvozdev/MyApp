using Microsoft.AspNetCore.Mvc;
using MyApp.Domain.DomainModel;
using MyApp.Domain.Services;

namespace MyApp.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
[Produces("application/json")]
public class HomeController : ControllerBase
{
    private readonly IPageRepository pageRepository;
    private readonly IPageCrudService pageCrudService;
    private readonly ISnippetRepository snippetRepository;
    private readonly ISnippetCrudService snippetCrudService;

    public HomeController(IPageRepository pageRepository, IPageCrudService pageCrud, ISnippetRepository snippetRepository, ISnippetCrudService snippetCrud)
    {
        this.pageRepository = pageRepository;
        this.pageCrudService = pageCrud;
        this.snippetRepository = snippetRepository;
        this.snippetCrudService = snippetCrud;
    }

    [HttpGet]
    public Task<List<Page>> GetPagesList()
    {
        return this.pageRepository.GetListAsync();
    }

    [HttpPost]
    public Task<Page> CreatePage(Page page)
    {
        return this.pageCrudService.Create(page);
    }

    [HttpGet]
    public Task<Page> GetPageById(int pageId)
    {
        return this.pageCrudService.GetById(pageId);
    }

    [HttpDelete]
    public Task<Page> DeletePage(int pageId)
    {
        return this.pageCrudService.Delete(pageId);
    }

    [HttpPut]
    public Task<Page> UpdatePage(int pageId, Page page)
    {
        return this.pageCrudService.Update(pageId, page);
    }

    [HttpGet]
    public Task<List<Snippet>> GetSnippetsList()
    {
        return this.snippetRepository.GetListAsync();
    }

    [HttpPost]
    public Task<Snippet> CreateSnippet(Snippet snippet)
    {
        return this.snippetCrudService.Create(snippet);
    }

    [HttpGet]
    public Task<Snippet> GetSnippetById(int snippetId)
    {
        return this.snippetCrudService.GetById(snippetId);
    }

    [HttpDelete]
    public Task<Snippet> DeleteSnippet(int snippetId)
    {
        return this.snippetCrudService.Delete(snippetId);
    }

    [HttpPut]
    public Task<Snippet> UpdateSnippet(int snippetId, Snippet snippet)
    {
        return this.snippetCrudService.Update(snippetId, snippet);
    }
}