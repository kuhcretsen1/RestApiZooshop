using ZooShop.Domain.Orders;
using ZooShop.Domain.Categorys;

    
namespace ZooShop.Domain.Products;
public class Product
{
    public ProductId Id { get; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public int StockQuantity { get; private set; }
    public int CategoryId { get; private set; }

    private Product(ProductId id, string name, string description, decimal price, int stockQuantity, int categoryId)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        StockQuantity = stockQuantity;
        CategoryId = categoryId;
    }

    public static Product New(ProductId id, string name, string description, decimal price, int stockQuantity, int categoryId)
        => new(id, name, description, price, stockQuantity, categoryId);

    public void UpdateDetails(string name, string description, decimal price, int stockQuantity)
    {
        Name = name;
        Description = description;
        Price = price;
        StockQuantity = stockQuantity;
    }

    public void AdjustStock(int quantity)
    {
        StockQuantity += quantity;
    }
}