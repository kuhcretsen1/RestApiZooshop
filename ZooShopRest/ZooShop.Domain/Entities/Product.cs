namespace ZooShop.Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }

    // Відношення
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
}