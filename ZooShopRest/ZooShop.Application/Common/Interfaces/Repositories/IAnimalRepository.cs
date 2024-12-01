using Optional;
using ZooShop.Domain.Animals;

namespace ZooShop.Application.Common.Interfaces.Repositories;

public interface IAnimalRepository
{
    Task<IReadOnlyList<Animal>> GetAll(CancellationToken cancellationToken);
    Task<Option<Animal>> GetById(AnimalId id, CancellationToken cancellationToken);
    Task<Option<Animal>> GetByName(string name, CancellationToken cancellationToken);
    Task<Animal> Add(Animal animal, CancellationToken cancellationToken);
    Task<Animal> Update(Animal animal, CancellationToken cancellationToken);
    Task<Animal> Delete(Animal animal, CancellationToken cancellationToken);
    Task<bool> Exists(AnimalId id, CancellationToken cancellationToken);
}