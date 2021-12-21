using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyApp.Domain;
using MyApp.Domain.DomainModel;

namespace MyApp.Controllers;
[ApiController]
[Route("api")]
[Produces("application/json")]
public class HomeController : ControllerBase
{
    private readonly ICreativeRepository creativeRepository;

    public HomeController(ICreativeRepository repository)
    {
        this.creativeRepository = repository;
    }

    [HttpGet]
    [Route("[controller]/[action]")]
    public async Task<List<Creative>> GetList()
    {
        return await this.creativeRepository.GetListAsync();
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
            await this.creativeRepository.Create(page);
        }
        catch (DbUpdateException ex)
        {
            if (ex.InnerException is SqlException sqlEx && sqlEx.Number == 2601)
            {
                return this.BadRequest("This name is already taken");
            }
            else
            {
                return this.BadRequest("Unhandled error");
            }
        }

        return this.Ok(page);
    }

    [HttpGet]
    [Route("[controller]/[action]")]
    public async Task<IActionResult> GetById(int pageId)
    {
        var page = await this.creativeRepository.Get(pageId);

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
        var page = await this.creativeRepository.Get(pageId);

        if (page == null)
        {
            return this.NotFound($"Page vith id: {pageId} not found");
        }

        await this.creativeRepository.Delete(page);

        return this.Ok("Item deleted");
    }

    [HttpPut]
    [Route("[controller]/[action]")]
    public async Task<IActionResult> Update(int pageId, Page page)
    {
        var oldPage = await this.creativeRepository.Get(pageId);

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
            await this.creativeRepository.Update(oldPage, page);
        }
        catch (DbUpdateException ex)
        {
            if (ex.InnerException is SqlException sqlEx && sqlEx.Number == 2601)
            {
                return this.BadRequest("This name is already taken");
            }
            else
            {
                return this.BadRequest("Unhandled error");
            }
        }

        return this.Ok(page);
    }
}