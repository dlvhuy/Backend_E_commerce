using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Ecommerce_BE.Models.Configuration
{
  public class UserConfiguration : IEntityTypeConfiguration<User>
  {
    public void Configure(EntityTypeBuilder<User> builder)
    {

      builder.HasKey(key => key.Id);
      builder.Property(e => e.Id)
              .ValueGeneratedOnAdd();

      builder.Property(e => e.UserName).HasMaxLength(128).IsRequired(true).IsUnicode();
      builder.Property(e => e.Email).HasMaxLength(128).IsRequired(true);
      builder.Property(e => e.PasswordHash).HasMaxLength(128).IsRequired(true);
      builder.Property(e => e.isSeller).IsRequired(true);
      builder.Property(e => e.isActive).IsRequired(true);
      builder.Property(e => e.PhoneNumber).HasMaxLength(10).IsRequired(true);

      builder.HasOne(u => u.Store)       
        .WithOne(p => p.User)              
        .HasForeignKey<Store>(p => p.IdUser);

      builder.HasMany(u => u.ProductRates)
        .WithOne(u => u.ProductUser)
        .HasForeignKey(u => u.IdUser)
        .OnDelete(DeleteBehavior.Cascade);
        ;   

    }
  }
}
