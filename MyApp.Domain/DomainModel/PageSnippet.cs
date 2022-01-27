namespace MyApp.Domain.DomainModel;
public class PageSnippet
{
    public int Id { get; set; }

    public int PageId { get; set; }

    public string SnippetName { get; set; }

    public PageSnippet(string snippetName)
    {
        this.SnippetName = snippetName;
    }
}
