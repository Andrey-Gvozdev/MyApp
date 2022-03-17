using Microsoft.AspNetCore.Mvc;
using MyApp.Domain.DomainModel;
using MyApp.Domain.Services.CRUDServices;

namespace MyApp.Controllers;
[ApiController]
[Route("/snippet/")]
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
    public Task<List<Snippet>> Get()
    {
        return this.snippetRepository.GetListAsync();
    }

    [HttpPost]
    public Task<Snippet> Post(Snippet snippet)
    {
        return this.snippetCrudService.Create(snippet);
    }

    [HttpGet("{snippetId}")]
    public Task<Snippet> Get(int snippetId)
    {
        return this.snippetCrudService.GetById(snippetId);
    }

    [HttpDelete]
    public Task<Snippet> Delete(int snippetId)
    {
        return this.snippetCrudService.Delete(snippetId);
    }

    [HttpPut]
    public Task<Snippet> Put(int snippetId, string content)
    {
        return this.snippetCrudService.Update(snippetId, content);
    }
}
