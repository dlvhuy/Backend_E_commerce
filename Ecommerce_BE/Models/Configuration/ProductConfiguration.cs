using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce_BE.Models.Configuration
{
  public class ProductConfiguration : IEntityTypeConfiguration<Product>
  {
    public void Configure(EntityTypeBuilder<Product> builder)
    {

      builder.HasKey(key => key.Id);

      builder.Property(e => e.Id)
              .ValueGeneratedOnAdd();

      builder.Property(x => x.Name).HasMaxLength(256).IsRequired().IsUnicode();
      builder.Property(x => x.Quantity).HasMaxLength(128).IsRequired();
      builder.Property(x => x.CostPrice).HasMaxLength(128).IsRequired();
      builder.Property(x => x.SellingPrice).HasMaxLength(128).IsRequired();
      builder.Property(x => x.Description).HasMaxLength(2048).IsRequired();
      builder.Property(x => x.Image).HasMaxLength(256).IsRequired();
      builder.Property(x => x.Rate).HasMaxLength(5).IsRequired();
      builder.Property(x => x.CreateTime).IsRequired();


      builder.HasMany(u => u.ProductRates)
       .WithOne(u => u.Product)
       .HasForeignKey(u => u.ProductId)
       .OnDelete(DeleteBehavior.ClientCascade);

    }
  }
}
