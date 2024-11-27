using ZooShop.Domain.Products;
using Optional;

namespace ZooShop.Common.Interfaces.Repositories;

public interface IProductRepository
{
    Task<Product> Add(Product product, CancellationToken cancellationToken);
    Task<Product> Update(Product product, CancellationToken cancellationToken);
    Task<Option<Product>> GetById(int id, CancellationToken cancellationToken);
    Task Delete(int id, CancellationToken cancellationToken);
}
