using ZooShop.Application.Commands;
using ZooShop.Application.Interfaces;
using ZooShop.Domain.Entities;
using ZooShop.Infrastructure;

namespace ZooShop.Application.Services
{
    public class CustomerServices : ICustomerServices
    {
        private readonly ZooShopDbContext _context;

        public CustomerServices(ZooShopDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> CreateCustomer(CreateCustomerCommand command)
        {
            var customer = new Customer
            {
                Name = command.Name,
                Email = command.Email,
                Address = command.Address,
                PhoneNumber = command.PhoneNumber
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer?> GetCustomerById(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<Customer?> UpdateCustomer(UpdateCustomerCommand command)
        {
            var customer = await _context.Customers.FindAsync(command.CustomerId);
            if (customer == null) return null;

            customer.Name = command.Name;
            customer.Email = command.Email;
            customer.Address = command.Address;
            customer.PhoneNumber = command.PhoneNumber;

            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<bool> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return false;

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}