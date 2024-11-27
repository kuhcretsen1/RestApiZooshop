using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZooShop.Domain.Orders;

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

           
        }
    }
}