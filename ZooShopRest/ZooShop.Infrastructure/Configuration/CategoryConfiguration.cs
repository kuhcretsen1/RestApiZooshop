using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZooShop.Domain.Entities;

namespace ZooShop.Infrastructure.Configuration;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        // Основні налаштування
        builder.HasKey(c => c.Id); // Первинний ключ
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100); // Назва обов'язкова, максимум 100 символів

        // Відношення з Animal
        builder.HasMany(c => c.Animals)
            .WithOne(a => a.Category)
            .HasForeignKey(a => a.CategoryId)
            .OnDelete(DeleteBehavior.Restrict); // Обмеження на видалення категорії (тварини залишаться)

        // Відношення з Product
        builder.HasMany(c => c.Products)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Cascade); // Якщо категорію видалено, продукти видаляються
    }
}