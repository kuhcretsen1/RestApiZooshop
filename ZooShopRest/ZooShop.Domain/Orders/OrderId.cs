namespace ZooShop.Domain.Orders;

public record OrderId(Guid Value)
{
    public static OrderId New() => new(Guid.NewGuid());
    public static OrderId Empty() => new(Guid.Empty);
    public override string ToString() => Value.ToString();
}