using ZooShop.Application.Common;
using ZooShop.Application.Common.Interfaces.Repositories;
using ZooShop.Application.Customers.Exceptions;
using ZooShop.Domain.Customers;
using MediatR;
namespace ZooShop.Application.Customers.Commands;
public record DeleteCustomerCommand : IRequest<Result<Customer, CustomerException>>
{
    public required Guid CustomerId { get; init; }
}
public class DeleteCustomerCommandHandler(ICustomerRepository customerRepository)
    : IRequestHandler<DeleteCustomerCommand, Result<Customer, CustomerException>>
{
    public async Task<Result<Customer, CustomerException>> Handle(
        DeleteCustomerCommand request,
        CancellationToken cancellationToken)
    {
        var customerId = new CustomerId(request.CustomerId);
        var existingCustomer = await customerRepository.GetById(customerId, cancellationToken);
        return await existingCustomer.Match<Task<Result<Customer, CustomerException>>>(
            async customer => await DeleteEntity(customer, cancellationToken),
            () => Task.FromResult<Result<Customer, CustomerException>>(new CustomerNotFoundException(customerId))
        );
    }
    private async Task<Result<Customer, CustomerException>> DeleteEntity(Customer customer, CancellationToken cancellationToken)
    {
        try
        {
            return await customerRepository.Delete(customer, cancellationToken);
        }
        catch (Exception exception)
        {
            return new CustomerUnknownException(customer.Id, exception);
        }
    }
}