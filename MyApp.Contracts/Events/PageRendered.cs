namespace MyApp.Contracts.Events;
public class PageRendered
{
    public int Id { get; set; }

    public int PageId { get; set; }

    public string Content { get; set; }

    public PageRendered(int pageId, string contentRendered)
    {
        PageId = pageId;
        Content = contentRendered;
    }
}
