using Swashbuckle.AspNetCore.Annotations;

namespace MyApp.Domain.DomainModel;
public class Snippet : Creative
{
    [SwaggerSchema(ReadOnly = true)]
    public List<Page>? Pages { get; set; } = new List<Page>();

    public Snippet(string name, string content)
    : base(name, content)
    {
    }
}
