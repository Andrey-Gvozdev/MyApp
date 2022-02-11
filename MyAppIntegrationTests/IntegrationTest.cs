using Moq;
using Rebus.Bus;
using System.Net.Http;
using Xunit;

namespace MyAppIntegrationTests;
[Trait("Category", "Integration")]
public abstract class IntegrationTest : IClassFixture<MyAppFactory>
{
    protected readonly MyAppFactory factory;
    protected readonly HttpClient client;
    protected readonly Mock<IBus> mockBus;

    public IntegrationTest(MyAppFactory fixture)
    {
        this.factory = fixture;
        this.client = factory.CreateClient();
        this.mockBus = factory.mockBus;
    }
}