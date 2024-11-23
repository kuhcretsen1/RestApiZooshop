using FluentValidation;
using ZooShop.Domain.Entities;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(p => p.Name).NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must be less than 100 characters.");
        RuleFor(p => p.Price).GreaterThan(0).WithMessage("Price must be greater than 0.");
        RuleFor(p => p.StockQuantity).GreaterThanOrEqualTo(0).WithMessage("Stock quantity must be non-negative.");
        RuleFor(p => p.CategoryId).NotEmpty().WithMessage("CategoryId is required.");
    }
}