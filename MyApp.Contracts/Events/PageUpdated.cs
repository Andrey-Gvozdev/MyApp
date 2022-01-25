namespace MyApp.Contracts.Events;
public class PageUpdated : IEvent
{
    public DateTime CreationTime { get; set; }

    public int Id { get; set; }

    public string Content { get; set; }

    public PageUpdated(int id, string contentRendered)
    {
        CreationTime = DateTime.UtcNow;
        Id = id;
        Content = contentRendered;
    }
}