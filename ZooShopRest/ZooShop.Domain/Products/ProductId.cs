namespace ZooShop.Domain.Products;

public record ProductId(Guid Value)
{
    public static ProductId New() => new(Guid.NewGuid());
    public static ProductId Empty() => new(Guid.Empty);
    public override string ToString() => Value.ToString();
}
