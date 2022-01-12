using Microsoft.AspNetCore.Mvc;
using MyApp.Domain.DomainModel;
using MyApp.Domain.Services;

namespace MyApp.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
[Produces("application/json")]
public class SnippetController : Controller
{
    private readonly ISnippetRepository snippetRepository;
    private readonly ISnippetCrudService snippetCrudService;

    public SnippetController(ISnippetRepository snippetRepository, ISnippetCrudService snippetCrud)
    {
        this.snippetRepository = snippetRepository;
        this.snippetCrudService = snippetCrud;
    }

    [HttpGet]
    public Task<List<Snippet>> GetList()
    {
        return this.snippetRepository.GetListAsync();
    }

    [HttpPost]
    public Task<Snippet> Create(Snippet snippet)
    {
        return this.snippetCrudService.Create(snippet);
    }

    [HttpGet]
    public Task<Snippet> GetById(int snippetId)
    {
        return this.snippetCrudService.GetById(snippetId);
    }

    [HttpDelete]
    public Task<Snippet> Delete(int snippetId)
    {
        return this.snippetCrudService.Delete(snippetId);
    }

    [HttpPut]
    public Task<Snippet> Update(int snippetId, Snippet snippet)
    {
        return this.snippetCrudService.Update(snippetId, snippet);
    }
}
