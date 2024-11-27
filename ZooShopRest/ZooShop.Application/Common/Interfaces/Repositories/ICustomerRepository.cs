using Optional;
using ZooShop.Domain.Customers;

namespace ZooShop.Application.Common.Interfaces.Repositories;

public interface ICustomerRepository
{
    Task<IReadOnlyList<Customer>> GetAll(CancellationToken cancellationToken);
    Task<Option<Customer>> GetById(CustomerId id, CancellationToken cancellationToken);
    Task<Option<Customer>> GetByEmail(string email, CancellationToken cancellationToken);
    Task<Customer> Add(Customer customer, CancellationToken cancellationToken);
    Task<Customer> Update(Customer customer, CancellationToken cancellationToken);
    Task<Customer> Delete(Customer customer, CancellationToken cancellationToken);
    Task<bool> Exists(CustomerId id, CancellationToken cancellationToken);
}