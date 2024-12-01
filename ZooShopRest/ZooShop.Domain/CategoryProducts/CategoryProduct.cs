using ZooShop.Domain.Categorys;
using ZooShop.Domain.Products;

namespace ZooShop.Domain.CategoryProducts;

public class CategoryProduct
{
    public CategoryProductId Id { get; private set; }
    public CategoryId CategoryId { get; }
    public ProductId ProductId { get; }
    
    public virtual Category Category { get; private set; }
    public virtual Product Product { get; private set; }

    public DateTime CreatedAt { get; }
    
    private CategoryProduct() { }
    
    // Constructor (for direct creation)
    public CategoryProduct(CategoryProductId id, CategoryId categoryId, ProductId productId, DateTime createdAt)
    {
        Id = id;
        CategoryId = categoryId;
        ProductId = productId;
        CreatedAt = createdAt;
    }

    // Factory method for creating a new instance
    public static CategoryProduct Link(CategoryId categoryId, ProductId productId)
        => new(
            new CategoryProductId(Guid.NewGuid()), 
            categoryId, 
            productId, 
            DateTime.UtcNow
        );
}