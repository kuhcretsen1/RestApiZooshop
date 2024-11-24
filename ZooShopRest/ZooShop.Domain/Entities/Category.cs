namespace ZooShop.Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty; // Назва категорії

    // Відношення
    public ICollection<Animal> Animals { get; set; } = new List<Animal>(); // Тварини в категорії
    public ICollection<Product> Products { get; set; } = new List<Product>(); // Продукти в категорії

    public Category(string name)
    {
        Name = name;
    }

    public void AddAnimal(Animal animal)
    {
        Animals.Add(animal);
    }

    public void AddProduct(Product product)
    {
        Products.Add(product);
    }
}