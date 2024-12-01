using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZooShop.Domain.Customers;

namespace ZooShop.Infrastructure.Configuration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            
            // Налаштування первинного ключа
            builder.HasKey(c => c.Id);
            
            builder.Property(cp => cp.Id)
                .HasConversion(id => id.Value, value => new CustomerId(value))
                .IsRequired();
            
            // Налаштування властивостей
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.PhoneNumber)
                .IsRequired()
                .HasMaxLength(15);
        }
    }
}