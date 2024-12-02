using Optional;
using ZooShop.Domain.Products;

namespace ZooShop.Application.Common.Interfaces.Repositories;

public interface IProductRepository
{
    Task<Product> Add(Product product, CancellationToken cancellationToken);
    Task<Product> Update(Product product, CancellationToken cancellationToken);
    Task<Option<Product>> GetById(ProductId id, CancellationToken cancellationToken);
    Task Delete(ProductId id, CancellationToken cancellationToken);
    Task<Option<Product>> GetByName(string requestName, CancellationToken cancellationToken);
}
