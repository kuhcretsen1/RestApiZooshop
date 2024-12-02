using ZooShop.Application.Common;
using ZooShop.Application.Common.Interfaces.Repositories;
using ZooShop.Application.Animals.Exceptions;
using ZooShop.Domain.Animals;
using ZooShop.Domain.Categorys;
using MediatR;
using Optional;

namespace ZooShop.Application.Animals.Commands;

public record UpdateAnimalCommand : IRequest<Result<Animal, AnimalException>>
{
    public required Guid AnimalId { get; init; }
    public required string Name { get; init; }
    public required string Species { get; init; }
    public required int Age { get; init; }
    public required decimal Price { get; init; }
    public required Guid CategoryId { get; init; }
}

public class UpdateAnimalCommandHandler(IAnimalRepository animalRepository, ICategoryRepository categoryRepository)
    : IRequestHandler<UpdateAnimalCommand, Result<Animal, AnimalException>>
{
    public async Task<Result<Animal, AnimalException>> Handle(UpdateAnimalCommand request, CancellationToken cancellationToken)
    {
        var animalId = new AnimalId(request.AnimalId);
        var animal = await animalRepository.GetById(animalId, cancellationToken);

        return await animal.Match(
            async a =>
            {
                var categoryId = new CategoryId(request.CategoryId);
                var category = await categoryRepository.GetById(categoryId, cancellationToken);

                return await category.Match(
                    async c => await UpdateEntity(a, request, cancellationToken),
                    () => Task.FromResult<Result<Animal, AnimalException>>(new AnimalCategoryNotFoundException(categoryId)));
            },
            () => Task.FromResult<Result<Animal, AnimalException>>(new AnimalNotFoundException(animalId)));
    }

    private async Task<Result<Animal, AnimalException>> UpdateEntity(
        Animal animal,
        UpdateAnimalCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            animal.Name = request.Name;
            animal.Species = request.Species;
            animal.Age = request.Age;
            animal.Price = request.Price;
            animal.CategoryId = new CategoryId(request.CategoryId);

            return await animalRepository.Update(animal, cancellationToken);
        }
        catch (Exception exception)
        {
            return new AnimalUnknownException(animal.Id, exception);
        }
    }
}