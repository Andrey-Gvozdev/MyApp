using MyApp.Domain.DomainModel;
using NUnit.Framework;
using FluentAssertions;

namespace MyAppTests;
[TestFixture]
public class PageTests
{
    [TestCase("content", "<!DOCTYPE html>\n<html>\n<head></head>\n<body>\ncontent\n</body>\n</html>")]
    [TestCase("<body>\ncontent\n</body>", "<!DOCTYPE html>\n<html>\n<head></head>\n<body>\ncontent\n</body>\n</html>")]
    [TestCase("\n<head></head>\n<body>\ncontent\n</body>", "<!DOCTYPE html>\n<html>\n<head></head>\n<body>\ncontent\n</body>\n</html>")]
    [TestCase("<body>\ncontent\n</body>\n<head></head>\n", "<!DOCTYPE html>\n<html>\n<head></head>\n<body>\ncontent\n</body>\n</html>")]
    public void HtmlCorrector_CombinationsOfHtmlTags_ReturnsTrue(string content, string expectedContent)
    {
        content = Page.HtmlCorrector(content);

        content.Should().Be(expectedContent);
    }
}
