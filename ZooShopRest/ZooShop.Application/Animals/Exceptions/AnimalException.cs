using ZooShop.Domain.Animals;
using ZooShop.Domain.Categorys;
namespace ZooShop.Application.Animals.Exceptions;
public abstract class AnimalException : Exception
{
    public AnimalId? AnimalId { get; }
    public CategoryId? CategoryId { get; }
    protected AnimalException(AnimalId? animalId, CategoryId? categoryId, string message, Exception? innerException = null)
        : base(message, innerException)
    {
        AnimalId = animalId;
        CategoryId = categoryId;

    }
}

public class AnimalNotFoundException : AnimalException
{
    public AnimalNotFoundException(AnimalId id)
        : base(id, null, $"Animal under id: {id} not found")

    {
    }
}

public class AnimalAlreadyExistsException : AnimalException
{
    public AnimalAlreadyExistsException(AnimalId id)
        : base(id, null, $"Animal already exists: {id}")

    {
    }
}

public class AnimalUnknownException : AnimalException
{
    public AnimalUnknownException(AnimalId id, Exception innerException)
        : base(id, null, $"Unknown exception for the animal under id: {id}", innerException)

    {
    }
}

public class AnimalCategoryNotFoundException : AnimalException
{
    public AnimalCategoryNotFoundException(CategoryId id)
        : base(null, id, $"Category under id: {id} not found")

    {
    }
}