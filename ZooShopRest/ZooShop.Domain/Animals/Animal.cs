using ZooShop.Domain.Categorys;

namespace ZooShop.Domain.Animals;



public class Animal
{
    public AnimalId Id { get; set; }
    public string Name { get; set; }  // Назва тварини
    public string Species { get; set; } // Вид тварини
    public int Age { get; set; } // Вік тварини
    public decimal Price { get; set; } // Ціна тварини

    // Відношення
    public CategoryId CategoryId { get; set; }
    public Category Category { get; set; } = null!; // Категорія (обов'язкова)

    public static Animal Create(string name, string species, int age, decimal price, CategoryId categoryId)
    {
        return new Animal(name, species, age, price, categoryId);
    }

    public Animal(string name, string species, int age, decimal price, CategoryId categoryId)
    {
        Name = name;
        Species = species;
        Age = age;
        Price = price;
        CategoryId = categoryId;
    }
}