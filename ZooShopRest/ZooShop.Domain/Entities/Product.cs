namespace ZooShop.Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty; // Назва продукту
    public decimal Price { get; set; } // Ціна продукту
    public int StockQuantity { get; set; } // Кількість на складі

    // Відношення
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!; // Категорія (обов'язкова)

    public Product(string name, decimal price, int stockQuantity, int categoryId)
    {
        Name = name;
        Price = price;
        StockQuantity = stockQuantity;
        CategoryId = categoryId;
    }
}