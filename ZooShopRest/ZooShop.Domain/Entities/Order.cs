namespace ZooShop.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }

    // Відношення
    public ICollection<Product> Products { get; set; } = new List<Product>();
}