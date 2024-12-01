using Microsoft.EntityFrameworkCore;
using Optional;
using ZooShop.Application.Common.Interfaces.Queries;
using ZooShop.Application.Common.Interfaces.Repositories;
using ZooShop.Domain.Animals;

namespace ZooShop.Infrastructure.Persistence.Repositories;

public class AnimalRepository : IAnimalRepository, IAnimalQueries
{
    private readonly ZooShopDbContext _context;

    public AnimalRepository(ZooShopDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Animal>> GetAll(CancellationToken cancellationToken)
    {
        return await _context.Animals
            .AsNoTracking()
            .Include(a => a.Category) // Завантаження категорії
            .ToListAsync(cancellationToken);
    }

    public async Task<Option<Animal>> GetById(AnimalId id, CancellationToken cancellationToken)
    {
        var entity = await _context.Animals
            .AsNoTracking()
            .Include(a => a.Category) // Завантаження категорії
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

        return entity == null ? Option.None<Animal>() : Option.Some(entity);
    }

    public async Task<Option<Animal>> GetByName(string name, CancellationToken cancellationToken)
    {
        var entity = await _context.Animals
            .AsNoTracking()
            .Include(a => a.Category) // Завантаження категорії
            .FirstOrDefaultAsync(a => a.Name == name, cancellationToken);

        return entity == null ? Option.None<Animal>() : Option.Some(entity);
    }

    public async Task<Animal> Add(Animal animal, CancellationToken cancellationToken)
    {
        await _context.Animals.AddAsync(animal, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return animal;
    }

    public async Task<Animal> Update(Animal animal, CancellationToken cancellationToken)
    {
        _context.Animals.Update(animal);
        await _context.SaveChangesAsync(cancellationToken);

        return animal;
    }

    public async Task<Animal> Delete(Animal animal, CancellationToken cancellationToken)
    {
        _context.Animals.Remove(animal);
        await _context.SaveChangesAsync(cancellationToken);

        return animal;
    }

    public async Task<bool> Exists(AnimalId id, CancellationToken cancellationToken)
    {
        return await _context.Animals.AnyAsync(a => a.Id == id, cancellationToken);
    }
}