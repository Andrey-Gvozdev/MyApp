namespace MyApp.Domain.DomainModel;
public class PageRendered
{
    public int Id { get; set; }

    public string Content { get; set; }

    public PageRendered(int id, string renderedContent)
    {
        this.Id = id;
        this.Content = renderedContent;
    }
}
