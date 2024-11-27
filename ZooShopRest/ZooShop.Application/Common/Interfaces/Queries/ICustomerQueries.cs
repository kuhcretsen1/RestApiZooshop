using Optional;
using ZooShop.Domain.Customers;

namespace ZooShop.Application.Common.Interfaces.Queries;

public interface ICustomerQueries
{
    Task<IReadOnlyList<Customer>> GetAll(CancellationToken cancellationToken);
    Task<Option<Customer>> GetById(CustomerId id, CancellationToken cancellationToken);
    Task<Option<Customer>> GetByEmail(string email, CancellationToken cancellationToken);
}