using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MyApp.Domain.DomainModel;

namespace MyApp.Domain.Services;
public class CreativeCrudService : ControllerBase, ICreativeCrudService
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
        try
        {
            this.validationService.ValidationNameLength(page.Name);
            this.validationService.ValidationNameIsFilled(page.Name);
            await this.validationService.ValidationNameIsUnique(page.Name);
            await this.creativeRepository.Create(page);
        }
        catch (ValidationException validationException)
        {
            throw new ValidationException(validationException.Message);
        }

        return page;
    }

    public async Task<Creative> GetById(int pageId)
    {
        var page = await this.creativeRepository.Get(pageId);

        try
        {
            this.validationService.ValidationCreativeIsNotNull(page, pageId);
        }
        catch (ValidationException validationException)
        {
            throw new ValidationException(validationException.Message);
        }

        return page;
    }

    public async Task<Creative> Delete(int pageId)
    {
        var page = await this.creativeRepository.Get(pageId);

        try
        {
            this.validationService.ValidationCreativeIsNotNull(page, pageId);
        }
        catch (ValidationException validationException)
        {
            throw new ValidationException(validationException.Message);
        }

        await this.creativeRepository.Delete(page);

        return page;
    }

    public async Task<Creative> Update(int pageId, Creative page)
    {
        var oldPage = await this.creativeRepository.Get(pageId);

        try
        {
            this.validationService.ValidationCreativeIsNotNull(oldPage, pageId);
            this.validationService.ValidationNameLength(page.Name);
            this.validationService.ValidationNameIsFilled(page.Name);
            await this.validationService.ValidationNameIsUnique(page.Name);
            await this.creativeRepository.Update(oldPage, page);
        }
        catch (ValidationException validationException)
        {
            throw new ValidationException(validationException.Message);
        }

        return page;
    }
}