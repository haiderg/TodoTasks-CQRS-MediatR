using FluentValidation;
namespace TodoTasks.Application.Features.Category.Commands.DeleteCategory;


public class DeleteCategoryCommandValidator: AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Category id must be greater than 0.");
    }
}