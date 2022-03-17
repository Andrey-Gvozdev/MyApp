﻿using Microsoft.AspNetCore.Mvc;
using MyApp.Domain.DomainModel;
using MyApp.Domain.Services.CRUDServices;

namespace MyApp.Controllers;

[ApiController]
[Route("/page/")]
[Produces("application/json")]
public class PageController : Controller
{
    private readonly IPageRepository pageRepository;
    private readonly IPageCrudService pageCrudService;

    public PageController(IPageRepository pageRepository, IPageCrudService pageCrud)
    {
        this.pageRepository = pageRepository;
        this.pageCrudService = pageCrud;
    }

    [HttpGet]
    public Task<List<Page>> Get()
    {
        return this.pageRepository.GetListAsync();
    }

    [HttpPost]
    [Route("/page/post/")]
    public Task<Page> Post(Page page)
    {
        return this.pageCrudService.Create(page);
    }

    [HttpGet]
    [Route("/page/get/{pageId}/")]
    public Task<Page> Get(int pageId)
    {
        return this.pageCrudService.GetById(pageId);
    }

    [HttpDelete]
    [Route("/page/delete/{pageId}")]
    public Task<Page> Delete(int pageId)
    {
        return this.pageCrudService.Delete(pageId);
    }

    [HttpPut]
    [Route("/page/put/{pageId}")]
    public Task<Page> Put(int pageId, Page page)
    {
        return this.pageCrudService.Update(pageId, page);
    }
}
