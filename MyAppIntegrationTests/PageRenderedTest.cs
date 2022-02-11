using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
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

        await client.SendAsync(postRequest);

        mockBus.Verify(x => x.Publish(It.Is<PageRendered>(e => e.Content == expectedContent), null), Times.Once);
    }

    [Fact]
    public async Task GetedPageIsExpectedPageTrue()
    {
        var expectedPage = new Page("expectedName", "content");
        var postRequest = new HttpRequestMessage(HttpMethod.Post, "/api/page/");
        var testPageJson = JsonConvert.SerializeObject(expectedPage);
        postRequest.Content = new StringContent(testPageJson, Encoding.UTF8, "application/json");
        var postResponce = JsonConvert.DeserializeObject<Page>((await client.SendAsync(postRequest)).Content.ReadAsStringAsync().Result);
        var getRequest = new HttpRequestMessage(HttpMethod.Get, $"/api/page/{postResponce.Id}");
        getRequest.Content = new StringContent(postResponce.Id.ToString(), Encoding.UTF8, "application/json");
        
        var getResponce = await client.SendAsync(getRequest);
        var result = JsonConvert.DeserializeObject<Page>((getResponce).Content.ReadAsStringAsync().Result);

        result.Name.Should().Be(expectedPage.Name);
    }
}