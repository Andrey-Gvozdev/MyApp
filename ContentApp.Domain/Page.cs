namespace ContentApp.Domain;
public class Page
{
    public int Id { get; private set; }

    public int PageId { get; private set; }

    public string Content { get; private set; }

    public Page(int pageId, string content)
    {
        this.PageId = pageId;
        this.Content = content;
    }
}