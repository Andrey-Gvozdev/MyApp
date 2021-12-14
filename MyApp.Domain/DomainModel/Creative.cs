namespace MyApp.Domain;
using Swashbuckle.AspNetCore.Annotations;

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

    public string Name
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
        if (name.Length > 30)
        {
            // ToDo "Name field must be less than 30 characters!"
        }
        else if (name != null || name != string.Empty)
        {
            this.name = name;
        }
        else
        {
            // ToDo "Name field is empty!"
        }
    }
}