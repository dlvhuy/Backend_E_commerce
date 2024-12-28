using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce_BE.Models.Configuration
{
  public class CategoryConfiguration : IEntityTypeConfiguration<Category>
  {
    public void Configure(EntityTypeBuilder<Category> builder)
    {
       builder.HasKey(x => x.Id);

      builder.Property(e => e.Id)
              .ValueGeneratedOnAdd();

      builder.Property(x => x.Name).HasMaxLength(256).IsUnicode().IsRequired();

      builder.HasMany(e => e.Products)
        .WithOne(e => e.Category)
        .HasForeignKey(e =>e.IdCategory);
    }
  }
}
