using Moq;
using MyApp.Domain.Services;
using NUnit.Framework;

namespace MyAppTests;

[TestFixture]
public class ValidationServiceTests
{
    [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
    [TestCase("")]
    public void ValidationNameLength_NotEmptyLess30Characters_ReturnsFalse(string name)
    {
        bool result = true;
        var mock = new Mock<IValidationService>();

        try
        {
            mock.Verify(x => x.ValidationNameLength(name));
        }
        catch
        {
            result = false;
        }

        Assert.False(result);
    }
}