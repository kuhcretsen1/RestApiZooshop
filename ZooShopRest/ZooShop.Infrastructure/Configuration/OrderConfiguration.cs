using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZooShop.Domain.Orders;
using ZooShop.Infrastructure.Converters;
namespace ZooShop.Infrastructure.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id); // Первинний ключ
            builder.Property(cp => cp.Id)
                .HasConversion(id => id.Value, value => new OrderId(value))
                .IsRequired();
            
            builder.Property(o => o.OrderDate)
                .IsRequired()
                .HasConversion(new DateTimeUtcConverter()) // Додаємо конвертер для збереження в UTC
                .HasColumnType("timestamp with time zone") // Задаємо тип поля для збереження з часовою зоною
                .HasDefaultValueSql("timezone('utc', now())"); // Задаємо значення за замовчуванням (UTC)


            builder.Property(o => o.TotalAmount)
                .IsRequired()
                .HasColumnType("decimal(18, 2)"); // Загальна сума замовлення

           
        }
    }
}