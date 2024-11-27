using Optional;
using ZooShop.Domain.Orders;

namespace ZooShop.Application.Common.Interfaces.Queries;

public interface IOrderQueries
{
    Task<IReadOnlyList<Order>> GetAll(CancellationToken cancellationToken);
    Task<Option<Order>> GetById(OrderId id, CancellationToken cancellationToken);
}