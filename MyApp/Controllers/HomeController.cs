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
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(Page page)
    {
        var checker = await this.pageRepository.Create(page);

        if (page.Name == null)
        {
            return this.BadRequest("Name field must be less than 30 characters and can't be empty!");
        }

        if (checker == null)
        {
            return this.BadRequest("This name is already taken!");
        }

        return this.Ok(page);
    }

    [HttpGet]
    [Route("[controller]/[action]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Page))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int pageId)
    {
        var page = await this.pageRepository.Get(pageId);
        if (page == null)
        {
            return this.NotFound();
        }

        return this.Ok(page);
    }

    [HttpDelete]
    [Route("[controller]/[action]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Page))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int pageId)
    {
        var page = await this.pageRepository.Get(pageId);

        if (page == null)
        {
            return this.NotFound();
        }

        await this.pageRepository.Delete(page);

        return this.Ok("Item deleted");
    }

    [HttpPut]
    [Route("[controller]/[action]")]
    [ProducesResponseType(StatusCodes.Status205ResetContent, Type = typeof(Page))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int pageId, Page page)
    {
        var oldPage = await this.pageRepository.Get(pageId);

        if (oldPage == null)
        {
            return this.NotFound();
        }

        if (page.Name == null)
        {
            return this.BadRequest("Name field must be less than 30 characters and can't be empty");
        }

        var item = await this.pageRepository.Update(oldPage, page);

        return item == null ? this.BadRequest("This name is already taken!") : this.Ok(page);
    }
}
