namespace ZooShop.Domain.Animals;

public record AnimalId(Guid Value)
{
    public static AnimalId New() => new(Guid.NewGuid());
    public static AnimalId Empty() => new(Guid.Empty);
    public override string ToString() => Value.ToString();
}
