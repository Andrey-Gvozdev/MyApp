﻿using MyApp.Domain.DomainModel;
using OpenQA.Selenium;

namespace MyApp.Domain.Services;
public class CreativeCrudService : ICreativeCrudService
{
    private readonly ICreativeRepository creativeRepository;
    private readonly IValidationService validationService;

    public CreativeCrudService(ICreativeRepository repository, IValidationService validation)
    {
        this.creativeRepository = repository;
        this.validationService = validation;
    }

    public async Task<Creative> Create(Creative page)
    {
        await this.validationService.ValidationCreativeName(page);
        await this.creativeRepository.Create(page);

        return page;
    }

    public async Task<Creative> GetById(int pageId)
    {
        var page = await this.GetPage(pageId);

        return page;
    }

    public async Task<Creative> Delete(int pageId)
    {
        var page = await this.GetPage(pageId);

        await this.creativeRepository.Delete(page);

        return page;
    }

    public async Task<Creative> Update(int pageId, Creative page)
    {
        await this.GetPage(pageId);

        page.Id = pageId;
        await this.validationService.ValidationCreativeName(page);
        await this.creativeRepository.Update(page);

        return page;
    }

    private async Task<Creative> GetPage(int pageId)
    {
        var page = await this.creativeRepository.Get(pageId);
        return page ?? throw new NotFoundException("Item not found");
    }
}