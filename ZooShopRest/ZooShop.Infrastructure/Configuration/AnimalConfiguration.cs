using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZooShop.Domain.Entities;

namespace ZooShop.Infrastructure.Configuration;

public class AnimalConfiguration : IEntityTypeConfiguration<Animal>
{
    public void Configure(EntityTypeBuilder<Animal> builder)
    {
        builder.HasKey(a => a.Id); // Первинний ключ
        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(150); // Назва тварини максимум 150 символів
        builder.Property(a => a.Species)
            .IsRequired()
            .HasMaxLength(100); // Вид обов'язковий
        builder.Property(a => a.Age)
            .IsRequired(); // Вік обов'язковий
        builder.Property(a => a.Price)
            .IsRequired()
            .HasColumnType("decimal(10, 2)"); // Ціна

        // Зв'язок з Category
        builder.HasOne(a => a.Category)
            .WithMany(c => c.Animals)
            .HasForeignKey(a => a.CategoryId)
            .OnDelete(DeleteBehavior.Restrict); // Категорія не видаляє тварин
    }
}