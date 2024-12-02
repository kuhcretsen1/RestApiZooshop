using FluentValidation;

namespace ZooShop.Application.Orders.Commands;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty();
        RuleFor(x => x.ProductIds).NotEmpty();
        RuleFor(x => x.TotalAmount).GreaterThan(0);
    }
}