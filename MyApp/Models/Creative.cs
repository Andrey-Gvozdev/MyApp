using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Creative
    {
        [Key]
        string Name { get; set; }
        string Content { get; set; }
    }
}
