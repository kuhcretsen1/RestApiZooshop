using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ZooShop.Application.Commands;
using ZooShop.Application.Interfaces;
using ZooShop.Domain.Entities;

namespace ZooShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerServices _customerService;
        private readonly IValidator<CreateCustomerCommand> _createCustomerValidator;
        private readonly IValidator<UpdateCustomerCommand> _updateCustomerValidator;

        public CustomerController(
            ICustomerServices customerService,
            IValidator<CreateCustomerCommand> createCustomerValidator,
            IValidator<UpdateCustomerCommand> updateCustomerValidator)
        {
            _customerService = customerService;
            _createCustomerValidator = createCustomerValidator;
            _updateCustomerValidator = updateCustomerValidator;
        }

        // Створити нового покупця
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand command)
        {
            var validationResult = await _createCustomerValidator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var customer = await _customerService.CreateCustomer(command);
            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, customer);
        }

        // Отримати покупця за ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await _customerService.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // Оновити покупця
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] UpdateCustomerCommand command)
        {
            if (id != command.CustomerId)
            {
                return BadRequest("Customer ID mismatch.");
            }

            var validationResult = await _updateCustomerValidator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var customer = await _customerService.UpdateCustomer(command);
            if (customer == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // Видалити покупця
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var success = await _customerService.DeleteCustomer(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}