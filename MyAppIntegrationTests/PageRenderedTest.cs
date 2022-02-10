using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Moq;
using MyApp.Contracts.Events;
using MyApp.Domain.DomainModel;
using Newtonsoft.Json;
using Rebus.Bus;
using Xunit;

namespace MyAppIntegrationTests;
public class PageRenderedTest : IntegrationTest
{
    public PageRenderedTest(MyAppFactory fixture)
        : base(fixture)
    {
    }

    [Fact]
    public async Task TriggeredPageRenderedEvent()
    {
        string expectedContent = "<!DOCTYPE html>\n<html>\n<head></head>\n<body>\ntestContent\n</body>\n</html>";
        var postRequest = new HttpRequestMessage(HttpMethod.Post, "/api/page/");
        var testPageJson = JsonConvert.SerializeObject(new Page("testName", "testContent"));
        postRequest.Content = new StringContent(testPageJson, Encoding.UTF8, "application/json");

        var mockBus = new Mock<IBus>();
        mockBus.Setup(x => x.Publish(It.Is<PageRendered>(message => message.Content == expectedContent), null));

        await client.SendAsync(postRequest);

        mockBus.Verify();
    }
}