using ZooShop.Domain.CategoryProducts;

namespace ZooShop.Api.Dtos;

public record CategoryProductDto(Guid? Id, Guid CategoryId, Guid ProductId, DateTime CreatedAt)
{
    public static CategoryProductDto FromDomainModel(CategoryProduct categoryProduct)
        => new(
            categoryProduct.Id.Value, 
            categoryProduct.CategoryId.Value, 
            categoryProduct.ProductId.Value, 
            categoryProduct.CreatedAt);
}