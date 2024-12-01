using Microsoft.EntityFrameworkCore;
using ZooShop.Infrastructure.Configuration;
using ZooShop.Domain.Animals;
using ZooShop.Domain.Categorys;
using ZooShop.Domain.Orders;
using ZooShop.Domain.Products;
using ZooShop.Domain.Customers;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace ZooShop.Infrastructure;

public class ZooShopDbContext : DbContext
{
    public DbSet<Animal> Animals { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;

    public ZooShopDbContext(DbContextOptions<ZooShopDbContext> options) : base(options) {}
    public DbSet<Customer> Customers { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ZooShopDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

}

