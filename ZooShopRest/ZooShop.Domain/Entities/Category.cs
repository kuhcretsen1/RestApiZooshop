namespace ZooShop.Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty; // Назва категорії

    // Відношення
    private readonly List<Animal> _animals = new();
    private readonly List<Product> _products = new();

    public IReadOnlyCollection<Animal> Animals => _animals.AsReadOnly(); // Тварини в категорії
    public IReadOnlyCollection<Product> Products => _products.AsReadOnly(); // Продукти в категорії

    public Category(string name)
    {
        Name = name;
    }

    public void AddAnimal(Animal animal)
    {
        _animals.Add(animal);
    }

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }
}