using FluentValidation;
using ZooShop.Application.Commands;

namespace ZooShop.Application.Validators
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must be less than 100 characters.");
            RuleFor(p => p.Price).GreaterThan(0).WithMessage("Price must be greater than 0.");
            RuleFor(p => p.StockQuantity).GreaterThanOrEqualTo(0).WithMessage("Stock quantity must be non-negative.");
            RuleFor(p => p.CategoryId).GreaterThan(0).WithMessage("CategoryId is required.");
        }
    }
}