using FluentValidation;
using ZooShop.Domain.Entities;

namespace ZooShop.Application.Validators;

public class OrderValidator : AbstractValidator<Order>
{
    public OrderValidator()
    {
        RuleFor(o => o.TotalAmount).GreaterThan(0).WithMessage("Total amount must be greater than 0.");
        RuleFor(o => o.Products).NotEmpty().WithMessage("At least one product must be added to the order.");
    }
}