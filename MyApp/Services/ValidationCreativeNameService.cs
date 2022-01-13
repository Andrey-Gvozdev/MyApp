using System.ComponentModel.DataAnnotations;
using MyApp.Domain.DomainModel;
using MyApp.Domain.Services;

namespace MyApp.Services;
public class ValidationCreativeNameService : IValidationCreativeNameService
{
    private readonly ICreativeRepository creativeRepository;

    public ValidationCreativeNameService(ICreativeRepository repository)
    {
        this.creativeRepository = repository;
    }

    public async Task ValidationCreativeName(Creative creative)
    {
        if (string.IsNullOrWhiteSpace(creative.Name))
        {
            throw new ValidationException("Name field is empty");
        }

        if (await this.creativeRepository.CreativeNameIsUnique(creative))
        {
            throw new ValidationException("This name is already taken");
        }

        if (creative.Name.Length > 30)
        {
            throw new ValidationException("Name field must be less than 30 characters");
        }
    }
}