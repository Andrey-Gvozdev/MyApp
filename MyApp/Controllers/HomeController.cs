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
    public Task<IActionResult> Create(Page page)
    {
        return this.creativeCrudService.Create(page);
    }

    [HttpGet]
    public Task<IActionResult> GetById(int pageId)
    {
        return this.creativeCrudService.GetById(pageId);
    }

    [HttpDelete]
    public Task<IActionResult> Delete(int pageId)
    {
        return this.creativeCrudService.Delete(pageId);
    }

    [HttpPut]
    public Task<IActionResult> Update(int pageId, Page page)
    {
        return this.creativeCrudService.Update(pageId, page);
    }
}