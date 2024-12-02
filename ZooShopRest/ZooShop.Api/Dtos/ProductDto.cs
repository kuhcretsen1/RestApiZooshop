using ZooShop.Domain.Products;

namespace ZooShop.Api.Dtos;

public record ProductDto(Guid? Id, string Name, decimal Price, Guid CategoryId)
{
    public static ProductDto FromDomainModel(Product product)
        => new(
            product.Id.Value, 
            product.Name, 
            product.Price, 
            product.CategoryId.Value);
}