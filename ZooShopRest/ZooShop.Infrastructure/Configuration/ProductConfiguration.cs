using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZooShop.Domain.Products;
using ZooShop.Domain.Categorys;

namespace ZooShop.Infrastructure.Configuration
{
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
                .HasColumnType("decimal(18, 2)"); // Ціна продукту

            // Перетворення для CategoryId (якщо це потрібно)
            builder.Property(p => p.CategoryId)
                .HasConversion(c => c.Value, c => new CategoryId(c)); // Перетворення для CategoryId
        }
    }
}