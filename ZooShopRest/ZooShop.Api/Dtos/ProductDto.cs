using ZooShop.Domain.Products;

namespace ZooShop.Api.Dtos;

public record ProductDto(
    Guid? Id,
    string Name,
    string Description,
    decimal Price,
    int StockQuantity,
    Guid CategoryId)
{
    public static ProductDto FromDomainModel(Product product)
        => new(
            product.Id.Value,
            product.Name,
            product.Description,
            product.Price,
            product.StockQuantity,
            product.CategoryId.Value);
}