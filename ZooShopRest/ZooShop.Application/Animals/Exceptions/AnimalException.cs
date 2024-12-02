using ZooShop.Domain.Animals;
using ZooShop.Domain.Categorys;

namespace ZooShop.Application.Animals.Exceptions;

public abstract class AnimalException : Exception
{
    public AnimalId AnimalId { get; }
    public CategoryId CategoryId { get; }

    protected AnimalException(AnimalId id, string message, Exception? innerException = null)
        : base(message, innerException)
    {
        AnimalId = id;
    }

    protected AnimalException(CategoryId id, string message, Exception? innerException = null)
        : base(message, innerException)
    {
        CategoryId = id;
    }
}

public class AnimalNotFoundException : AnimalException
{
    public AnimalNotFoundException(AnimalId id)
        : base(id, $"Animal under id: {id} not found")
    {
    }
}

public class AnimalAlreadyExistsException : AnimalException
{
    public AnimalAlreadyExistsException(AnimalId id)
        : base(id, $"Animal already exists: {id}")
    {
    }
}

public class AnimalUnknownException : AnimalException
{
    public AnimalUnknownException(AnimalId id, Exception innerException)
        : base(id, $"Unknown exception for the animal under id: {id}", innerException)
    {
    }
}

public class AnimalCategoryNotFoundException : AnimalException
{
    public AnimalCategoryNotFoundException(CategoryId id)
        : base(id, $"Category under id: {id} not found")
    {
    }
}