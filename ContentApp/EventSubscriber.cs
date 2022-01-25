using MyApp.Contracts.Events;
using Rebus.Bus;

namespace ContentApp;
public class EventSubscriber : IHostedService
{
    private readonly IBus bus;

    public EventSubscriber(IBus bus)
    {
        this.bus = bus;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        return this.SubscribeEvents();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private async Task SubscribeEvents()
    {
        await Task.WhenAll(
            this.bus.Subscribe<PageCreated>(),
            this.bus.Subscribe<PageUpdated>(),
            this.bus.Subscribe<SnippetCreated>(),
            this.bus.Subscribe<SnippetUpdated>());
    }
}
