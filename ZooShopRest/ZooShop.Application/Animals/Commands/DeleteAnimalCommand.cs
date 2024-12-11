using ZooShop.Application.Common;
using ZooShop.Application.Common.Interfaces.Repositories;
using ZooShop.Application.Animals.Exceptions;
using ZooShop.Domain.Animals;
using MediatR;

namespace ZooShop.Application.Animals.Commands;

public record DeleteAnimalCommand : IRequest<Result<Animal, AnimalException>>
{
    public required Guid AnimalId { get; init; }
}

public class DeleteAnimalCommandHandler(IAnimalRepository animalRepository)
    : IRequestHandler<DeleteAnimalCommand, Result<Animal, AnimalException>>
{
    public async Task<Result<Animal, AnimalException>> Handle(
        DeleteAnimalCommand request,
        CancellationToken cancellationToken)
    {
        var animalId = new AnimalId(request.AnimalId);
        var existingAnimal = await animalRepository.GetById(animalId, cancellationToken);

        return await existingAnimal.Match<Task<Result<Animal, AnimalException>>>(
            async animal => await DeleteEntity(animal, cancellationToken),
            () => Task.FromResult<Result<Animal, AnimalException>>(new AnimalNotFoundException(animalId))
        );
    }

    private async Task<Result<Animal, AnimalException>> DeleteEntity(Animal animal, CancellationToken cancellationToken)
    {
        try
        {
            return await animalRepository.Delete(animal, cancellationToken);
        }
        catch (Exception exception)
        {
            return new AnimalUnknownException(animal.Id, exception);
        }
    }
}