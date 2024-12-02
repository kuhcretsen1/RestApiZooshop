using FluentValidation;

namespace ZooShop.Application.Animals.Commands;

public class CreateAnimalCommandValidator : AbstractValidator<CreateAnimalCommand>
{
    public CreateAnimalCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(150);
        RuleFor(x => x.Species).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Age).GreaterThan(0);
        RuleFor(x => x.Price).GreaterThan(0);
        RuleFor(x => x.CategoryId).NotEmpty();
    }
}