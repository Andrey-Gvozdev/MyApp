using Microsoft.AspNetCore.Mvc;
using MyApp.Domain;
using MyApp.Domain.DomainModel;
using MyApp.Domain.Services;

namespace MyApp.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
[Produces("application/json")]
public class HomeController : ControllerBase
{
    private readonly ICreativeRepository creativeRepository;
    private readonly ICreativeCrudService creativeCrudService;

    public HomeController(ICreativeRepository repository, ICreativeCrudService crud)
    {
        this.creativeRepository = repository;
        this.creativeCrudService = crud;
    }

    [HttpGet]
    public Task<List<Creative>> GetList()
    {
        return this.creativeRepository.GetListAsync();
    }

    [HttpPost]
    public Task<Creative> CreatePage(Page page)
    {
        return this.creativeCrudService.Create(page);
    }

    [HttpPost]
    public Task<Creative> CreateSnippet(Snippet snippet)
    {
        return this.creativeCrudService.Create(snippet);
    }

    [HttpGet]
    public Task<Creative> GetById(int pageId)
    {
        return this.creativeCrudService.GetById(pageId);
    }

    [HttpDelete]
    public Task<Creative> Delete(int pageId)
    {
        return this.creativeCrudService.Delete(pageId);
    }

    [HttpPut]
    public Task<Creative> UpdatePage(int pageId, Page page)
    {
        return this.creativeCrudService.Update(pageId, page);
    }

    [HttpPut]
    public Task<Creative> UpdateSnippet(int snippetId, Snippet snippet)
    {
        return this.creativeCrudService.Update(snippetId, snippet);
    }
}