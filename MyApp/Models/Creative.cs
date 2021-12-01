using Microsoft.EntityFrameworkCore;

namespace MyProject.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Creative
    {
        string Name { get; set; }
        string Content { get; set; }
    }
}
