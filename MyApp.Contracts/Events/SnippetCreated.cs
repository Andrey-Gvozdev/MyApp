namespace MyApp.Contracts.Events;
public class SnippetCreated : IEvent
{
    public DateTime CreationTime { get; set; }

    public int Id { get; set; }

    public string Content { get; set; }

    public SnippetCreated(int id, string contentRendered)
    {
        CreationTime = DateTime.UtcNow;
        Id = id;
        Content = contentRendered;
    }
}
