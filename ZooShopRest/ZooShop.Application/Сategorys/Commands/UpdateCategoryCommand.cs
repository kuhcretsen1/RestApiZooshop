using ZooShop.Application.Common;
using ZooShop.Application.Common.Interfaces.Repositories;
using ZooShop.Application.Сategorys.Exceptions;
using ZooShop.Domain.Categorys;
using MediatR;
using Optional;

namespace ZooShop.Application.Сategorys.Commands;

public record UpdateCategoryCommand : IRequest<Result<Category, CategoryException>>
{
    public required Guid CategoryId { get; init; }
    public required string Name { get; init; }
}

public class UpdateCategoryCommandHandler(ICategoryRepository categoryRepository)
    : IRequestHandler<UpdateCategoryCommand, Result<Category, CategoryException>>
{
    public async Task<Result<Category, CategoryException>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryId = new CategoryId(request.CategoryId);
        var category = await categoryRepository.GetById(categoryId, cancellationToken);

        return await category.Match(
            async c =>
            {
                var existingCategory = await CheckDuplicated(categoryId, request.Name, cancellationToken);

                return await existingCategory.Match(
                    ec => Task.FromResult<Result<Category, CategoryException>>(new CategoryAlreadyExistsException(ec.Id)),
                    async () => await UpdateEntity(c, request, cancellationToken));
            },
            () => Task.FromResult<Result<Category, CategoryException>>(new CategoryNotFoundException(categoryId)));
    }

    private async Task<Result<Category, CategoryException>> UpdateEntity(
        Category category,
        UpdateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            category.UpdateDetails(request.Name);

            return await categoryRepository.Update(category, cancellationToken);
        }
        catch (Exception exception)
        {
            return new CategoryUnknownException(category.Id, exception);
        }
    }

    private async Task<Option<Category>> CheckDuplicated(
        CategoryId categoryId,
        string name,
        CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByName(name, cancellationToken);

        return category.Match(
            c => c.Id == categoryId ? Option.None<Category>() : Option.Some(c),
            Option.None<Category>);
    }
}