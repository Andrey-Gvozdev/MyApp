using Swashbuckle.AspNetCore.Annotations;

namespace MyApp.Domain;
public abstract class Creative
{
    [SwaggerSchema(ReadOnly = true)]
    public int Id { get; set; }

    public string Content { get; }

    public string Name { get; private set; }

    public Creative(string name, string content)
    {
        this.Name = name;
        this.Content = content;
    }
}