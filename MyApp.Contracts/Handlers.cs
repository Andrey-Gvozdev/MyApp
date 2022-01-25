using MyApp.Contracts.Events;
using Rebus.Handlers;

namespace MyApp.Contracts;
public class PageCreatedHandler : IHandleMessages<PageCreated>
{
    public Task Handle(PageCreated message)
    {
        throw new NotImplementedException();
    }
}

public class PageUpdatedHandler : IHandleMessages<PageUpdated>
{
    public Task Handle(PageUpdated message)
    {
        throw new NotImplementedException();
    }
}

public class SnippetUpdatedHandler : IHandleMessages<SnippetUpdated>
{
    public Task Handle(SnippetUpdated message)
    {
        throw new NotImplementedException();
    }
}

public class SnippetCreatedHandler : IHandleMessages<SnippetCreated>
{
    public Task Handle(SnippetCreated message)
    {
        throw new NotImplementedException();
    }
}
