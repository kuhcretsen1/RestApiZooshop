
using ZooShop.Domain.Categorys;
using System.Threading;
using System.Threading.Tasks;
using Optional;

namespace ZooShop.Application.Common.Interfaces.Repositories;

public interface ICategoryRepository
{
    Task<IReadOnlyList<Category>> GetAll(CancellationToken cancellationToken);
    Task<Option<Category>> GetById(CategoryId id, CancellationToken cancellationToken);
    Task<Option<Category>> GetByName(string name, CancellationToken cancellationToken);
    Task<Category> Add(Category category, CancellationToken cancellationToken);
    Task<Category> Update(Category category, CancellationToken cancellationToken);
    Task<Category> Delete(Category category, CancellationToken cancellationToken);
    Task<bool> Exists(CategoryId id, CancellationToken cancellationToken);
}
