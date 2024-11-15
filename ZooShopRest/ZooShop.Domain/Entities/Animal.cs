namespace ZooShop.Domain.Entities;

public class Animal
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Species { get; set; } = string.Empty;
    public int Age { get; set; }
    public decimal Price { get; set; }
    
    // Відношення
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
}