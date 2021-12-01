using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Models
{
    [Index(nameof(Name), IsUnique = true)]
    [Index(nameof(Content))]
    public class Creative
    {
        [Key]
        string Name { get; set; }

        [Required]
        string Content { get; set; }
    }
}
