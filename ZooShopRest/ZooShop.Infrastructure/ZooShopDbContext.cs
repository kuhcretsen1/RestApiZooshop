using Microsoft.EntityFrameworkCore;
using ZooShop.Domain.Entities;
using ZooShop.Infrastructure.Configuration;

namespace ZooShop.Infrastructure;

public class ZooShopDbContext : DbContext
{
    public DbSet<Animal> Animals { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;

    public ZooShopDbContext(DbContextOptions<ZooShopDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AnimalConfiguration());    }
}

