using ZooShop.Domain.Customers;
namespace ZooShop.Api.Dtos;

public record CustomerDto(Guid? Id, string Name, string Email, string Address, string PhoneNumber)
{
    public static CustomerDto FromDomainModel(Customer customer)
        => new(
            customer.Id.Value, 
            customer.Name, 
            customer.Email, 
            customer.Address, 
            customer.PhoneNumber);
}