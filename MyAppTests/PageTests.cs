using MyApp.Domain.DomainModel;
using NUnit.Framework;
using FluentAssertions;

namespace MyAppTests;
[TestFixture]
public class PageTests
{
    private Page MakePage(string content)
    {
        return new Page("name", content);
    }

    [TestCase("content", "<!DOCTYPE html>\n<html>\n<head></head>\n<body>\ncontent\n</body>\n</html>")]
    [TestCase("<body>\ncontent\n</body>", "<!DOCTYPE html>\n<html>\n<head></head>\n<body>\ncontent\n</body>\n</html>")]
    [TestCase("\n<head></head>\n<body>\ncontent\n</body>", "<!DOCTYPE html>\n<html>\n<head></head>\n<body>\ncontent\n</body>\n</html>")]
    [TestCase("<body>\ncontent\n</body>\n<head></head>\n", "<!DOCTYPE html>\n<html>\n<head></head>\n<body>\ncontent\n</body>\n</html>")]
    public void HtmlCorrector_CombinationsOfHtmlTags_ReturnsTrue(string content, string expectedContent)
    {
        var page = MakePage(content);

        page.Content.Should().Be(expectedContent);
    }
}
