using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce_BE.Models.Configuration
{
  public class UserProductRateConfiguration : IEntityTypeConfiguration<UserProductRate>
  {
    public void Configure(EntityTypeBuilder<UserProductRate> builder)
    {
      builder.HasKey(x => new { x.ProductId,x.IdUser});
    }
  }
}
