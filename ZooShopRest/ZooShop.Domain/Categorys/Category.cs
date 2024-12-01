using ZooShop.Domain.Categorys;
using ZooShop.Domain.Animals;
using ZooShop.Domain.Products;



namespace ZooShop.Domain.Categorys;

public class Category
{
    public CategoryId Id { get; }
    
    public string Name { get; private set; }

    private readonly List<Animal> _animals = new();
    public IReadOnlyCollection<Animal> Animals => _animals.AsReadOnly();

    private readonly List<Product> _products = new();
    public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

    protected  Category(CategoryId id, string name)
    {
        Id = id;
        Name = name;
    }

    public static Category New(CategoryId id, string name) => new(id, name);

    public void AddAnimal(Animal animal) => _animals.Add(animal);

    public void AddProduct(Product product) => _products.Add(product);

    public void UpdateDetails(string name) => Name = name;
}