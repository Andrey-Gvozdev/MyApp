using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MyApp.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Creative
    {
        [Key]
        public string Name { get; set; }
        public string Content { get; set; }
    }
}
