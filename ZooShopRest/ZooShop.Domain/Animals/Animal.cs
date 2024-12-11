using System.Text.Json.Serialization;
using ZooShop.Domain.Categorys;

namespace ZooShop.Domain.Animals;



public class Animal
{
    public AnimalId Id { get; set; }
    public string Name { get; set; }  
    public string Species { get; set; } 
    public int Age { get; set; } 
    public decimal Price { get; set; } 

    // Відношення
    public CategoryId CategoryId { get; set; }
    
    public Category? Category { get; }  

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