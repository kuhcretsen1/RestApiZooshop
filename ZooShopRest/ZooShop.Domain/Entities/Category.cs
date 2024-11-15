namespace ZooShop.Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    // Відношення
    public ICollection<Animal> Animals { get; set; } = new List<Animal>();
    public ICollection<Product> Products { get; set; } = new List<Product>();
}