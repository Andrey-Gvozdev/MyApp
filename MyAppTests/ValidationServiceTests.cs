using Infrastructure;
using MyApp.Domain.Services;
using NUnit.Framework;

namespace MyAppTests;

[TestFixture]
public class ValidationServiceTests
{
    private static IValidationService MakeValidationService()
    {
        return new ValidationService();
    }
    [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", false)]
    [TestCase("validName", true)]
    public void ValidationNameLength_WhenCalled_ChangesWasNameValid(string name, bool expected)
    {
        bool result = true;
        IValidationService validationService = MakeValidationService();

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

    [TestCase("", false)]
    [TestCase(" ", false)]
    [TestCase("validName", true)]
    public void ValidationNameIsFilled_WhenCalled_ChangesWasNameValid(string name, bool expected)
    {
        bool result = true;
        IValidationService validationService = MakeValidationService();

        try
        {
            validationService.ValidationNameIsFilled(name);
        }
        catch
        {
            result = false;
        }

        Assert.AreEqual(result, expected);
    }
}