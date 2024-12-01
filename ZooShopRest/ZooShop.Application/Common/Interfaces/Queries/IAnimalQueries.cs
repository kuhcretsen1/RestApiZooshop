using Optional;
using ZooShop.Domain.Animals;

namespace ZooShop.Application.Common.Interfaces.Queries;

public interface IAnimalQueries
{
    Task<IReadOnlyList<Animal>> GetAll(CancellationToken cancellationToken);
    Task<Option<Animal>> GetById(AnimalId id, CancellationToken cancellationToken);
    Task<Option<Animal>> GetByName(string name, CancellationToken cancellationToken);
}