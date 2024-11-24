using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZooShop.Domain.Entities;

namespace ZooShop.Infrastructure.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id); // Первинний ключ
            builder.Property(o => o.OrderDate)
                .IsRequired()
                .HasColumnType("datetime"); // Дата замовлення обов'язкова

            builder.Property(o => o.TotalAmount)
                .IsRequired()
                .HasColumnType("decimal(18, 2)"); // Загальна сума замовлення

            // Відношення з Product (Багато до багатьох)
            builder.HasMany(o => o.Products)
                .WithMany(p => p.Orders)
                .UsingEntity(j => j.ToTable("OrderProducts")); // Проміжна таблиця для зв'язку
        }
    }
}