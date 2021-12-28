using Infrastructure;
using Moq;
using MyApp.Domain.Services;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;

namespace MyAppTests;

[TestFixture]
public class ValidationServiceTests
{
    [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", false)]
    [TestCase("validName", true)]
    public void ValidationNameLength_WhenCalled_ChangesWasNameValid(string name, bool expected)
    {
        bool result = true;
        IValidationService validationService = new ValidationService();

        try
        {
            validationService.ValidationNameLength(name);
        }
        catch
        {
            result = false;
        }

        Assert.AreEqual(result, expected);
    }
}