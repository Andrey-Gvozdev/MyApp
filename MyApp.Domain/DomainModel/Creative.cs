using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace MyApp.Domain;
public abstract class Creative : IValidatableObject
{
    [SwaggerSchema(ReadOnly = true)]
    public int Id { get; set; }

    public string Content { get; private set; }

    public string Name { get; private set; }

    public Creative(string name, string content)
    {
        this.Name = name;
        this.Content = content;
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(this.Name))
        {
            yield return new ValidationResult("Name field is empty");
        }

        if (this.Name.Length > 30)
        {
            yield return new ValidationResult("Name field must be less than 30 characters");
        }
    }
}