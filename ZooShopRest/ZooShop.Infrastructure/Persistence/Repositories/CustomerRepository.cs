using Microsoft.EntityFrameworkCore;
using Optional;
using ZooShop.Application.Common.Interfaces.Queries;
using ZooShop.Application.Common.Interfaces.Repositories;
using ZooShop.Domain.Customers;

namespace ZooShop.Infrastructure.Persistence.Repositories;

public class CustomerRepository : ICustomerRepository, ICustomerQueries
{
    private readonly ZooShopDbContext _context;

    public CustomerRepository(ZooShopDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Customer>> GetAll(CancellationToken cancellationToken)
    {
        return await _context.Customers
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Option<Customer>> GetById(CustomerId id, CancellationToken cancellationToken)
    {
        var entity = await _context.Customers
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        return entity == null ? Option.None<Customer>() : Option.Some(entity);
    }

    public async Task<Option<Customer>> GetByEmail(string email, CancellationToken cancellationToken)
    {
        var entity = await _context.Customers
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Email == email, cancellationToken);

        return entity == null ? Option.None<Customer>() : Option.Some(entity);
    }

    public async Task<Customer> Add(Customer customer, CancellationToken cancellationToken)
    {
        await _context.Customers.AddAsync(customer, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return customer;
    }

    public async Task<Customer> Update(Customer customer, CancellationToken cancellationToken)
    {
        _context.Customers.Update(customer);
        await _context.SaveChangesAsync(cancellationToken);

        return customer;
    }

    public async Task<Customer> Delete(Customer customer, CancellationToken cancellationToken)
    {
        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync(cancellationToken);

        return customer;
    }

    public async Task<bool> Exists(CustomerId id, CancellationToken cancellationToken)
    {
        return await _context.Customers.AnyAsync(c => c.Id == id, cancellationToken);
    }
}