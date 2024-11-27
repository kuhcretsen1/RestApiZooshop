using Optional;
using ZooShop.Domain.Orders;

namespace ZooShop.Application.Common.Interfaces.Repositories;

public interface IOrderRepository
{
    Task<IReadOnlyList<Order>> GetAll(CancellationToken cancellationToken);
    Task<Option<Order>> GetById(OrderId id, CancellationToken cancellationToken);
    Task<Order> Add(Order order, CancellationToken cancellationToken);
    Task<Order> Update(Order order, CancellationToken cancellationToken);
    Task<Order> Delete(Order order, CancellationToken cancellationToken);
    Task<bool> Exists(OrderId id, CancellationToken cancellationToken);
}