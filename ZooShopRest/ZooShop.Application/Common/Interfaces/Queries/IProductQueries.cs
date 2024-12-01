using ZooShop.Domain.Products;
using ZooShop.Application.Common.Interfaces;
using Optional;

namespace ZooShop.Application.Common.Interfaces.Queries;

public interface IProductQueries
{
    Task<Product> Add(Product product, CancellationToken cancellationToken);
    Task<Product> Update(Product product, CancellationToken cancellationToken);
    Task<Option<Product>> GetById(ProductId id, CancellationToken cancellationToken);
    Task Delete(ProductId id, CancellationToken cancellationToken);
}
