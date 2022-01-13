using System.ComponentModel.DataAnnotations.Schema;
using Swashbuckle.AspNetCore.Annotations;

namespace MyApp.Domain.DomainModel;
public abstract class Creative
{
    [SwaggerSchema(ReadOnly = true)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Content { get; protected set; }

    public string Name { get; protected set; }

    public Creative(string name, string content)
    {
        this.SetName(name);
        this.SetContent(content);
    }

    public virtual void SetContent(string content)
    {
        this.Content = content;
    }

    public virtual void SetName(string name)
    {
        this.Name = name;
    }
}