namespace ContentApp.Domain;
public class Page
{
    public int Id { get; private set; }

    public string Content { get; private set; }

    public Page()
    {
    }

    public Page(int id, string renderedContent)
    {
        this.Id = id;
        this.Content = renderedContent;
    }
}