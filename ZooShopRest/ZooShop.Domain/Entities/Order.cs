namespace ZooShop.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; } // Дата замовлення
    public decimal TotalAmount { get; set; } // Загальна сума замовлення

    // Відношення
    private readonly List<Product> _products = new();
    public IReadOnlyCollection<Product> Products => _products.AsReadOnly(); // Продукти в замовленні

    public Order(decimal totalAmount)
    {
        OrderDate = DateTime.UtcNow;
        TotalAmount = totalAmount;
    }

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }
}