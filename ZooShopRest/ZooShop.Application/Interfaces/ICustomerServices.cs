using ZooShop.Application.Commands;
using ZooShop.Domain.Entities;

namespace ZooShop.Application.Interfaces
{
    public interface ICustomerServices
    {
        Task<Customer> CreateCustomer(CreateCustomerCommand command);
        Task<Customer?> GetCustomerById(int id);
        Task<Customer?> UpdateCustomer(UpdateCustomerCommand command);
        Task<bool> DeleteCustomer(int id);
    }
}