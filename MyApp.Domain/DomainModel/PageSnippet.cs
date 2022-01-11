namespace MyApp.Domain.DomainModel
{
    public class PageSnippet
    {
        public int Id { get; set; }

        public int PageId { get; set; }

        public int SnippetId { get; set; }

        public PageSnippet(int snippetId)
        {
            this.SnippetId = snippetId;
        }
    }
}
