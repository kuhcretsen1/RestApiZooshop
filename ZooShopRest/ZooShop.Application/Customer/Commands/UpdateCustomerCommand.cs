using ZooShop.Application.Common;
using ZooShop.Application.Common.Interfaces.Repositories;
using ZooShop.Application.Customers.Exceptions;
using ZooShop.Domain.Customers;
using MediatR;
using Optional;
namespace ZooShop.Application.Customers.Commands;
public record UpdateCustomerCommand : IRequest<Result<Customer, CustomerException>>
{
    public required Guid CustomerId { get; init; }
    public required string Name { get; init; }
    public required string Email { get; init; }
    public required string Address { get; init; }
    public required string PhoneNumber { get; init; }
}
public class UpdateCustomerCommandHandler(ICustomerRepository customerRepository)
    : IRequestHandler<UpdateCustomerCommand, Result<Customer, CustomerException>>
{
    public async Task<Result<Customer, CustomerException>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customerId = new CustomerId(request.CustomerId);
        var customer = await customerRepository.GetById(customerId, cancellationToken);
        return await customer.Match(
            async c =>
            {
                var existingCustomer = await CheckDuplicated(customerId, request.Email, cancellationToken);
                return await existingCustomer.Match(
                    ec => Task.FromResult<Result<Customer, CustomerException>>(new CustomerAlreadyExistsException(ec.Id)),
                    async () => await UpdateEntity(c, request, cancellationToken));
            },
            () => Task.FromResult<Result<Customer, CustomerException>>(new CustomerNotFoundException(customerId)));
    }
    private async Task<Result<Customer, CustomerException>> UpdateEntity(
        Customer customer,
        UpdateCustomerCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            customer.UpdateDetails(request.Name, request.Email, request.Address, request.PhoneNumber);
            return await customerRepository.Update(customer, cancellationToken);
        }
        catch (Exception exception)
        {
            return new CustomerUnknownException(customer.Id, exception);
        }
    }
    private async Task<Option<Customer>> CheckDuplicated(
        CustomerId customerId,
        string email,
        CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetByEmail(email, cancellationToken);
        return customer.Match(
            c => c.Id == customerId ? Option.None<Customer>() : Option.Some(c),
            Option.None<Customer>);
    }
}