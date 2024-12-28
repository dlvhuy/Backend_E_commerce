using Microsoft.EntityFrameworkCore;
using System;

namespace Ecommerce_BE.Models
{
  public class DbEContext : DbContext
  {
    public DbEContext(DbContextOptions<DbEContext> options) : base(options){ }
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Store> Stores { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbEContext).Assembly);
    }

  }
}
