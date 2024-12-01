namespace ZooShop.Domain.CategoryProducts;

public record CategoryProductId(Guid Value)
{
    public static CategoryProductId Empty => new(Guid.Empty);
    public static CategoryProductId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}