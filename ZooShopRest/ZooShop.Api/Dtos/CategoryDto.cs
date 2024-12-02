using ZooShop.Domain.Categorys;
using ZooShop.Domain.Animals;
using ZooShop.Domain.Products;

namespace ZooShop.Api.Dtos;

public record CategoryDto(Guid? Id, string Name, List<AnimalDto> Animals, List<ProductDto> Products)
{
    public static CategoryDto FromDomainModel(Category category)
        => new(
            category.Id.Value, 
            category.Name, 
            category.Animals.Select(a => AnimalDto.FromDomainModel(a)).ToList(),
            category.Products.Select(p => ProductDto.FromDomainModel(p)).ToList());
}