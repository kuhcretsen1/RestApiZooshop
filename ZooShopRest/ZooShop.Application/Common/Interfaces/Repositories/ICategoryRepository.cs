
using ZooShop.Domain.Categorys;
using System.Threading;
using System.Threading.Tasks;

namespace ZooShop.Application.Common.Interfaces.Repositories;

public interface ICategoryRepository
{
    Task<IReadOnlyList<Category>> GetAll(CancellationToken cancellationToken);
    Task<Category?> GetById(int id, CancellationToken cancellationToken);
    Task<Category> Add(Category category, CancellationToken cancellationToken);
    Task<Category> Update(Category category, CancellationToken cancellationToken);
    Task<Category> Delete(Category category, CancellationToken cancellationToken);
}
