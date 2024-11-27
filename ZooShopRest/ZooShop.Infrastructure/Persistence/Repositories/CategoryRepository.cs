using Microsoft.EntityFrameworkCore;
using Optional;
using ZooShop.Application.Common.Interfaces.Queries;
using ZooShop.Application.Common.Interfaces.Repositories;
using ZooShop.Domain.Categorys;

namespace ZooShop.Infrastructure.Persistence.Repositories;

public class CategoryRepository : ICategoryRepository, ICategoryQueries
{
    private readonly ZooShopDbContext _context;

    public CategoryRepository(ZooShopDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Category>> GetAll(CancellationToken cancellationToken)
    {
        return await _context.Categories
            .AsNoTracking()
            .Include(c => c.Animals)  // Завантаження зв'язку з Animal
            .Include(c => c.Products) // Завантаження зв'язку з Product
            .ToListAsync(cancellationToken);
    }

    public async Task<Option<Category>> GetById(CategoryId id, CancellationToken cancellationToken)
    {
        var entity = await _context.Categories
            .AsNoTracking()
            .Include(c => c.Animals)  // Завантаження зв'язку з Animal
            .Include(c => c.Products) // Завантаження зв'язку з Product
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        return entity == null ? Option.None<Category>() : Option.Some(entity);
    }

    public async Task<Option<Category>> GetByName(string name, CancellationToken cancellationToken)
    {
        var entity = await _context.Categories
            .AsNoTracking()
            .Include(c => c.Animals)  // Завантаження зв'язку з Animal
            .Include(c => c.Products) // Завантаження зв'язку з Product
            .FirstOrDefaultAsync(c => c.Name == name, cancellationToken);

        return entity == null ? Option.None<Category>() : Option.Some(entity);
    }

    public async Task<Category> Add(Category category, CancellationToken cancellationToken)
    {
        await _context.Categories.AddAsync(category, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return category;
    }

    public async Task<Category> Update(Category category, CancellationToken cancellationToken)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync(cancellationToken);

        return category;
    }

    public async Task<Category> Delete(Category category, CancellationToken cancellationToken)
    {
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync(cancellationToken);

        return category;
    }

    public async Task<bool> Exists(CategoryId id, CancellationToken cancellationToken)
    {
        return await _context.Categories.AnyAsync(c => c.Id == id, cancellationToken);
    }
}