using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZooShop.Domain.CategoryProducts;
using ZooShop.Domain.Categorys;
using ZooShop.Domain.Products;

public class CategoryProductConfiguration : IEntityTypeConfiguration<CategoryProduct>
{
    public void Configure(EntityTypeBuilder<CategoryProduct> builder)
    {
        builder.HasKey(cp => cp.Id);

        builder.Property(cp => cp.Id)
            .HasConversion(id => id.Value, value => new CategoryProductId(value))
            .IsRequired();

        builder.Property(cp => cp.CategoryId)
            .HasConversion(id => id.Value, value => new CategoryId(value))
            .IsRequired();

        builder.Property(cp => cp.ProductId)
            .HasConversion(id => id.Value, value => new ProductId(value))
            .IsRequired();

        builder.Property(cp => cp.CreatedAt)
            .HasDefaultValueSql("timezone('utc', now())")
            .IsRequired();

        builder.HasOne(cp => cp.Category)
            .WithMany()
            .HasForeignKey(cp => cp.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(cp => cp.Product)
            .WithMany()
            .HasForeignKey(cp => cp.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}