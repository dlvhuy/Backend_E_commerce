using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce_BE.Models.Configuration
{
  public class StoreConfiguration : IEntityTypeConfiguration<Store>
  {
    public void Configure(EntityTypeBuilder<Store> builder)
    {
      builder.HasKey(x => x.Id);
      builder.Property(e => e.Id)
               .ValueGeneratedOnAdd();


      builder.Property(x => x.Name).HasMaxLength(256).IsUnicode().IsRequired();
      builder.Property(x => x.Image).HasMaxLength(512).IsRequired();
      builder.Property(x => x.Address).HasMaxLength(256).IsRequired();
      builder.Property(x => x.Rate).IsRequired();
      builder.Property(x => x.Description).HasMaxLength(2048).IsRequired();
      builder.Property(x => x.CreateTime).IsRequired();

      builder.HasMany(x => x.Products)
        .WithOne(e => e.Store)
        .HasForeignKey(e => e.IdStore);


    }
  }
}
