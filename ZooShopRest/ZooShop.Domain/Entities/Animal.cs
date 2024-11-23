namespace ZooShop.Domain.Entities;

public class Animal
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty; // Назва тварини
    public string Species { get; set; } = string.Empty; // Вид тварини
    public int Age { get; set; } // Вік тварини
    public decimal Price { get; set; } // Ціна тварини

    // Відношення
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!; // Категорія (обов'язкова)

    public Animal(string name, string species, int age, decimal price, int categoryId)
    {
        Name = name;
        Species = species;
        Age = age;
        Price = price;
        CategoryId = categoryId;
    }
}