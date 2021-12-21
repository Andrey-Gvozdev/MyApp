using Microsoft.AspNetCore.Mvc;
using MyApp.Domain;
using MyApp.Domain.DomainModel;

namespace MyApp.Controllers;
[ApiController]
[Route("api")]
[Produces("application/json")]
public class HomeController : ControllerBase
{
    private readonly ICreativeRepository pageRepository;

    public HomeController(ICreativeRepository repository)
    {
        this.pageRepository = repository;
    }

    [HttpGet]
    [Route("[controller]/[action]")]
    public async Task<List<Creative>> GetList()
    {
        return await this.pageRepository.GetListAsync();
    }

    [HttpPost]
    [Route("[controller]/[action]")]
    public async Task<IActionResult> Create(Page page)
    {
        if (!this.ModelState.IsValid)
        {
            return this.BadRequest(this.ModelState);
        }

        try
        {
            await this.pageRepository.Create(page);
        }
        catch
        {
            return this.BadRequest("This name is already taken");
        }

        return this.Ok(page);
    }

    [HttpGet]
    [Route("[controller]/[action]")]
    public async Task<IActionResult> GetById(int pageId)
    {
        var page = await this.pageRepository.Get(pageId);

        if (page == null)
        {
            return this.NotFound($"Page vith id: {pageId} not found");
        }

        return this.Ok(page);
    }

    [HttpDelete]
    [Route("[controller]/[action]")]
    public async Task<IActionResult> Delete(int pageId)
    {
        var page = await this.pageRepository.Get(pageId);

        if (page == null)
        {
            return this.NotFound($"Page vith id: {pageId} not found");
        }

        await this.pageRepository.Delete(page);

        return this.Ok("Item deleted");
    }

    [HttpPut]
    [Route("[controller]/[action]")]
    public async Task<IActionResult> Update(int pageId, Page page)
    {
        var oldPage = await this.pageRepository.Get(pageId);

        if (oldPage == null)
        {
            return this.NotFound($"Page vith id: {pageId} not found");
        }

        if (!this.ModelState.IsValid)
        {
            return this.BadRequest(this.ModelState);
        }

        try
        {
            await this.pageRepository.Update(oldPage, page);
        }
        catch
        {
            return this.BadRequest("This name is already taken");
        }

        return this.Ok(page);
    }
}
