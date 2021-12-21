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
    public async Task<List<Creative>> GetList()
    {
        return await this.creativeRepository.GetListAsync();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Page page)
    {
        return await this.creativeCrudService.Create(page);
    }

    [HttpGet]
    public async Task<IActionResult> GetById(int pageId)
    {
        return await this.creativeCrudService.GetById(pageId);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int pageId)
    {
        return await this.creativeCrudService.Delete(pageId);
    }

    [HttpPut]
    public async Task<IActionResult> Update(int pageId, Page page)
    {
        return await this.creativeCrudService.Update(pageId, page);
    }
}