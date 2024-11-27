using Microsoft.EntityFrameworkCore;
using ZooShop.Domain.Products;
using ZooShop.Application.Common.Interfaces.Repositories;
using Optional;
using ZooShop.Common.Interfaces.Repositories;

namespace ZooShop.Infrastructure.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ZooShopDbContext _context;

    public ProductRepository(ZooShopDbContext context)
    {
        _context = context;
    }

    public async Task<Product> Add(Product product, CancellationToken cancellationToken)
    {
        await _context.Products.AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return product;
    }

    public async Task<Product> Update(Product product, CancellationToken cancellationToken)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync(cancellationToken);
        return product;
    }

    public async Task<Option<Product>> GetById(int id, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        return product == null ? Option.None<Product>() : Option.Some(product);
    }

    public async Task Delete(int id, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FindAsync(new object[] { id }, cancellationToken);

        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

}