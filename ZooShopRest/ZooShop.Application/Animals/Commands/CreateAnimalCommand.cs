using ZooShop.Application.Common;
using ZooShop.Application.Common.Interfaces.Repositories;
using ZooShop.Application.Animals.Exceptions;
using ZooShop.Application.Сategorys.Exceptions;
using ZooShop.Domain.Animals;
using MediatR;
using ZooShop.Domain.Categorys;
namespace ZooShop.Application.Animals.Commands;
public record CreateAnimalCommand : IRequest<Result<Animal, AnimalException>>
{
    public required string Name { get; init; }
    public required string Species { get; init; }
    public required int Age { get; init; }
    public required decimal Price { get; init; }
    public required Guid CategoryId { get; init; }
}
public class CreateAnimalCommandHandler : IRequestHandler<CreateAnimalCommand, Result<Animal, AnimalException>>
{
    private readonly IAnimalRepository _animalRepository;
    private readonly ICategoryRepository _categoryRepository;
    public CreateAnimalCommandHandler(IAnimalRepository animalRepository, ICategoryRepository categoryRepository)
    {
        _animalRepository = animalRepository;
        _categoryRepository = categoryRepository;
    }
    public async Task<Result<Animal, AnimalException>> Handle(CreateAnimalCommand request, CancellationToken cancellationToken)
    {
        var categoryId = new CategoryId(request.CategoryId);
        var category = await _categoryRepository.GetById(categoryId, cancellationToken);
        return await category.Match<Task<Result<Animal, AnimalException>>>(
            async c =>
            {
                var existingAnimal = await _animalRepository.GetByName(request.Name, cancellationToken);
                return await existingAnimal.Match(
                    animal => Task.FromResult<Result<Animal, AnimalException>>(new AnimalAlreadyExistsException(animal.Id)),
                    async () => await CreateEntity(request.Name, request.Species, request.Age, request.Price, categoryId, cancellationToken));
            },
            () => Task.FromResult(Result<Animal, AnimalException>.Failure(new AnimalCategoryNotFoundException(categoryId))));
    }
    
    private async Task<Result<Animal, AnimalException>> CreateEntity(
        string name,
        string species,
        int age,
        decimal price,
        CategoryId categoryId,
        CancellationToken cancellationToken)
    {
        try
        {
            var entity = Animal.Create(name, species, age, price, categoryId);
            return await _animalRepository.Add(entity, cancellationToken);
        }
        catch (Exception exception)
        {
            return Result<Animal, AnimalException>.Failure(new AnimalUnknownException(AnimalId.Empty(), exception));
        }
    }
}