using ZooShop.Application.Common;
using ZooShop.Application.Common.Interfaces.Repositories;
using ZooShop.Application.Customers.Exceptions;
using ZooShop.Domain.Customers;
using MediatR;

namespace ZooShop.Application.Customers.Commands;

public record CreateCustomerCommand : IRequest<Result<Customer, CustomerException>>
{
    public required string Name { get; init; }
    public required string Email { get; init; }
    public required string Address { get; init; }
    public required string PhoneNumber { get; init; }
}

public class CreateCustomerCommandHandler(ICustomerRepository customerRepository) 
    : IRequestHandler<CreateCustomerCommand, Result<Customer, CustomerException>>
{
    public async Task<Result<Customer, CustomerException>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var existingCustomer = await customerRepository.GetByEmail(request.Email, cancellationToken);

        return await existingCustomer.Match(
            c => Task.FromResult<Result<Customer, CustomerException>>(new CustomerAlreadyExistsException(c.Id)),
            async () => await CreateEntity(request.Name, request.Email, request.Address, request.PhoneNumber, cancellationToken));
    }

    private async Task<Result<Customer, CustomerException>> CreateEntity(
        string name,
        string email,
        string address,
        string phoneNumber,
        CancellationToken cancellationToken)
    {
        try
        {
            var entity = Customer.New(CustomerId.New(), name, email, address, phoneNumber);

            return await customerRepository.Add(entity, cancellationToken);
        }
        catch (Exception exception)
        {
            return new CustomerUnknownException(CustomerId.Empty(), exception);
        }
    }
}