using ZooShop.Domain.Categorys;
using ZooShop.Domain.Animals;
using ZooShop.Domain.Products;

namespace ZooShop.Api.Dtos;

public record AnimalDto(Guid? Id, string Name, string Species, int Age, decimal Price, Guid CategoryId)
{
    public static AnimalDto FromDomainModel(Animal animal)
        => new(
            animal.Id.Value, 
            animal.Name, 
            animal.Species, 
            animal.Age, 
            animal.Price, 
            animal.CategoryId.Value);
}