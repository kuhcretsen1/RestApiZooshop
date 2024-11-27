using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZooShop.Domain.Categorys;

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
        builder.HasMany(c => c.Animals) // Кожна категорія має багато тварин
            .WithOne(a => a.Category) // Кожна тварина належить одній категорії
            .HasForeignKey(a => a.CategoryId) // Зв'язок через CategoryId в тварині
            .OnDelete(DeleteBehavior.Restrict); // Обмеження на видалення категорії (тварини не видаляються)

        // Відношення з Product
        builder.HasMany(c => c.Products) // Кожна категорія має багато продуктів
            .WithOne(p => p.CategoryId) // Кожен продукт належить одній категорії
            .HasForeignKey(p => p.CategoryId) // Зв'язок через CategoryId в продукті
            .OnDelete(DeleteBehavior.Cascade); // Якщо категорію видалено, продукти видаляються
    }
}