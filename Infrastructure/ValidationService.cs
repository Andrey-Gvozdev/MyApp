﻿using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Services;

namespace Infrastructure;
public class ValidationService : IValidationService
{
    private readonly ApplicationDbContext db;

    public ValidationService(ApplicationDbContext context)
    {
        this.db = context;
    }

    public async Task ValidationNameIsUnique(string name)
    {
        if (await this.db.Creatives.AnyAsync(x => x.Name == name))
        {
            throw new ValidationException("This name is already taken");
        }
    }

    public void ValidationNameIsFilled(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ValidationException("Name field is empty");
        }
    }

    public void ValidationNameLength(string name)
    {
        if (name.Length > 30)
        {
            throw new ValidationException("Name field must be less than 30 characters");
        }
    }
}