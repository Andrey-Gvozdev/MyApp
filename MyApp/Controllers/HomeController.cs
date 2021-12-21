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
        var result = await this.creativeCrudService.Create(page);

        return result;
    }

    [HttpGet]
    public async Task<IActionResult> GetById(int pageId)
    {
        var result = await this.creativeCrudService.GetById(pageId);

        return result;
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int pageId)
    {
        var result = await this.creativeCrudService.Delete(pageId);

        return result;
    }

    [HttpPut]
    public async Task<IActionResult> Update(int pageId, Page page)
    {
        var result = await this.creativeCrudService.Update(pageId, page);

        return result;
    }
}