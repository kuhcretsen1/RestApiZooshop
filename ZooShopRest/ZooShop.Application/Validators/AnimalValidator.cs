using FluentValidation;
using ZooShop.Domain.Entities;

public class AnimalValidator : AbstractValidator<Animal>
{
    public AnimalValidator()
    {
        RuleFor(a => a.Name).NotEmpty().MaximumLength(150);
        RuleFor(a => a.Species).NotEmpty();
        RuleFor(a => a.Age).GreaterThan(0);
        RuleFor(a => a.Price).GreaterThan(0);
    }
}