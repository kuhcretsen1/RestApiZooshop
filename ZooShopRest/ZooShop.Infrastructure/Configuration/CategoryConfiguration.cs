using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZooShop.Domain.Categorys;
using ZooShop.Domain.Animals;
using ZooShop.Domain.Products;

namespace ZooShop.Infrastructure.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id); // Первинний ключ
            builder.Property(c => c.Id)
                .HasConversion(c => c.Value, c => new CategoryId(c)); // Перетворення для CategoryId

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100); // Назва категорії обов'язкова, максимум 100 символів

            // Відношення з Animal
            builder.HasMany(c => c.Animals)
                .WithOne(a => a.Category)
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); // Заборона каскадного видалення

           
        }
    }
}