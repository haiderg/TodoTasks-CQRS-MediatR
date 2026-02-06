using FluentValidation;

namespace TodoTasks.Application.Features.TodoTask.Commands.CreateTodoTask;

public class CreateTodoTaskCommandValidator : AbstractValidator<CreateTodoTaskCommand>
{
    public CreateTodoTaskCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(50).WithMessage("Title cannot exceed 50 characters");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters")
            .When(x => x.Description != null);

        RuleFor(x => x.DueDate)
            .GreaterThan(DateTime.UtcNow).WithMessage("Due date must be in future.")
            .When(x => x.DueDate.HasValue);

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("Category ID must be greater than 0")
            .When(x => x.CategoryId.HasValue);

        RuleFor(x => x.AssignedTo)
           .GreaterThan(0).WithMessage("Assigned to Id must be greater than 0")
           .When(x => x.AssignedTo.HasValue);

        RuleFor(x => x.ReminderAt)
           .GreaterThan(DateTime.UtcNow).WithMessage("Reminder date must be in future")
           .When(x => x.AssignedTo.HasValue);
    }
}
