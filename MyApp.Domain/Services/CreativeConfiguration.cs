using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyApp.Domain.Services;
public class CreativeConfiguration : IEntityTypeConfiguration<Creative>
{
    public void Configure(EntityTypeBuilder<Creative> builder)
    {
        builder.HasIndex(p => p.Name).IsUnique();
    }
}
