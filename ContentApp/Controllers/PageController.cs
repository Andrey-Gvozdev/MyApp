using ContentApp.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ContentApp.Controllers;
[ApiController]
[Route("content/page")]
[Produces("application/json")]
public class PageController : Controller
{
    private readonly IPageRepository pageRepository;

    public PageController(IPageRepository pageRepository)
    {
        this.pageRepository = pageRepository;
    }

    [HttpGet("{id}")]
    public Task<Page> Get(int id)
    {
        return this.pageRepository.Get(id);
    }
}