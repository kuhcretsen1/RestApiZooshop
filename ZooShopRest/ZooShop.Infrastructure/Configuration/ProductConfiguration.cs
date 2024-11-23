using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZooShop.Domain.Entities;

namespace ZooShop.Infrastructure.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id); // Первинний ключ
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200); // Назва продукту обов'язкова, максимум 200 символів
        builder.Property(p => p.Price)
            .IsRequired()
            .HasColumnType("decimal(10, 2)"); // Ціна з точністю до копійок
        builder.Property(p => p.StockQuantity)
            .IsRequired(); // Кількість обов'язкова

        // Зв'язок з Category
        builder.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Cascade); // Якщо категорія видаляється, продукти теж видаляються
    }
}