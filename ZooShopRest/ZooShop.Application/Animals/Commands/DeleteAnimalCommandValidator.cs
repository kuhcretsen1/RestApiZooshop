using FluentValidation;

namespace ZooShop.Application.Animals.Commands;

public class DeleteAnimalCommandValidator : AbstractValidator<DeleteAnimalCommand>
{
    public DeleteAnimalCommandValidator()
    {
        RuleFor(x => x.AnimalId).NotEmpty();
    }
}