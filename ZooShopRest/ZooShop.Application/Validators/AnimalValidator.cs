using FluentValidation;
using ZooShop.Domain.Entities;

namespace ZooShop.Application.Validators;

public class AnimalValidator : AbstractValidator<Animal>
{
    public AnimalValidator()
    {
        RuleFor(a => a.Name).NotEmpty().WithMessage("Name is required.")
            .MaximumLength(150).WithMessage("Name must be less than 150 characters.");
        RuleFor(a => a.Species).NotEmpty().WithMessage("Species is required.");
        RuleFor(a => a.Age).GreaterThan(0).WithMessage("Age must be greater than 0.");
        RuleFor(a => a.Price).GreaterThan(0).WithMessage("Price must be greater than 0.");
        RuleFor(a => a.CategoryId).GreaterThan(0).WithMessage("CategoryId is required.");
    }
}