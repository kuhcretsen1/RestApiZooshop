using FluentValidation;
using ZooShop.Application.Commands;

namespace ZooShop.Application.Validators;

public class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Email).NotEmpty().EmailAddress();
        RuleFor(c => c.Address).NotEmpty();
        RuleFor(c => c.PhoneNumber).NotEmpty();
    }
}