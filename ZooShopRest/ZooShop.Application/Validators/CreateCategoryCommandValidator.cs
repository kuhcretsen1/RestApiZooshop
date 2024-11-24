using FluentValidation;
using ZooShop.Application.Commands;

namespace ZooShop.Application.Validators
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(100).WithMessage("Category name must be less than 100 characters.");
        }
    }
}