using Microsoft.EntityFrameworkCore;
using Optional;
using ZooShop.Application.Common.Interfaces.Queries;
using ZooShop.Application.Common.Interfaces.Repositories;
using ZooShop.Domain.Orders;

namespace ZooShop.Infrastructure.Persistence.Repositories;

public class OrderRepository : IOrderRepository, IOrderQueries
{
    private readonly ZooShopDbContext _context;

    public OrderRepository(ZooShopDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Order>> GetAll(CancellationToken cancellationToken)
    {
        return await _context.Orders
            .AsNoTracking()
            .Include(o => o.Products) // Завантаження пов'язаних продуктів
            .ToListAsync(cancellationToken);
    }

    public async Task<Option<Order>> GetById(OrderId id, CancellationToken cancellationToken)
    {
        var entity = await _context.Orders
            .AsNoTracking()
            .Include(o => o.Products) // Завантаження пов'язаних продуктів
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);

        return entity == null ? Option.None<Order>() : Option.Some(entity);
    }

    public async Task<Order> Add(Order order, CancellationToken cancellationToken)
    {
        await _context.Orders.AddAsync(order, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return order;
    }

    public async Task<Order> Update(Order order, CancellationToken cancellationToken)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync(cancellationToken);

        return order;
    }

    public async Task<Order> Delete(Order order, CancellationToken cancellationToken)
    {
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync(cancellationToken);

        return order;
    }

    public async Task<bool> Exists(OrderId id, CancellationToken cancellationToken)
    {
        return await _context.Orders.AnyAsync(o => o.Id == id, cancellationToken);
    }
}