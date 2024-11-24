using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZooShop.Domain.Entities;

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

            // Відношення з Order (Багато до багатьох)
            builder.HasMany(p => p.Orders)
                .WithMany(o => o.Products)
                .UsingEntity(j => j.ToTable("OrderProducts")); // Проміжна таблиця для зв'язку
        }
    }
}