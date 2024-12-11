using Microsoft.EntityFrameworkCore;
using ZooShop.Domain.Products;
using ZooShop.Application.Common.Interfaces.Repositories;
using Optional;
using ZooShop.Application.Common;
using ZooShop.Application.Common.Interfaces.Queries;
using ZooShop.Application.Products.Exceptions;

namespace ZooShop.Infrastructure.Persistence.Repositories;

public class ProductRepository : IProductRepository, IProductQueries
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

    Task IProductQueries.Delete(ProductId id, CancellationToken cancellationToken)
    {
        return Delete(id, cancellationToken);
    }

    public async Task<Option<Product>> GetByName(string name, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Name == name, cancellationToken);

        // Використовуємо Option.Some або Option.None залежно від результату
        return product != null ? Option.Some(product) : Option.None<Product>();
    }



    public async Task<Option<Product>> GetById(ProductId id, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        return product == null ? Option.None<Product>() : Option.Some(product);
    }
    

    public async Task<Result<Product, ProductException>> Delete(ProductId id, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FindAsync(new object[] { id }, cancellationToken);

        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync(cancellationToken);
            return Result<Product, ProductException>.Success(product);
        }
        else
        {
            return Result<Product, ProductException>.Failure(new ProductNotFoundException(id));
        }
    }
    
}