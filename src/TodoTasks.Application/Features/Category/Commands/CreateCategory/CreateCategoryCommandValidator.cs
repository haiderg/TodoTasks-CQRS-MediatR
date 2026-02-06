using FluentValidation;

namespace TodoTasks.Application.Features.Category.Commands.CreateCategory;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category Name is required.")
            .MaximumLength(30).WithMessage("Maximum 30 characters are allowed.");
    }
}