using Swashbuckle.AspNetCore.Annotations;

namespace MyApp.Domain;
public abstract class Creative
{
    private string name;
    private string content;

    [SwaggerSchema(ReadOnly = true)]
    public int Id { get; set; }

    public string Content
    {
        get { return this.content; }
    }

    public string? Name
    {
        get { return this.name; }
        set { this.SetName(value); }
    }

    public Creative(string name, string content)
    {
        this.content = content;
        this.SetName(name);
    }

    private void SetName(string name)
    {
        if (name.Length > 30 || name == string.Empty)
        {
            return;
        }
        else
        {
            this.name = name;
        }
    }
}