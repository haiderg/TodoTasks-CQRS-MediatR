using FluentValidation;

namespace TodoTasks.Application.Features.TodoTask.Commands.UpdateTodoTask;

public class UpdateTodoTaskCommandValidator : AbstractValidator<UpdateTodoTaskCommand>
{
    public UpdateTodoTaskCommandValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(50)
            .When(x => !string.IsNullOrWhiteSpace(x.Title));

        RuleFor(x => x.Description)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.Description));

        RuleFor(x => x.DueDate)
            .GreaterThanOrEqualTo(DateTime.UtcNow)
            .When(x => x.DueDate.HasValue);

        RuleFor(x => x.CategoryId)
            .GreaterThan(0)
            .When(x => x.CategoryId.HasValue);

        RuleFor(x => x.AssignedTo)
          .GreaterThan(0).WithMessage("Assigned to Id must be greater than 0")
          .When(x => x.AssignedTo.HasValue);

        RuleFor(x => x.ReminderAt)
           .GreaterThan(DateTime.UtcNow).WithMessage("Reminder date must be in future")
           .When(x => x.AssignedTo.HasValue);
    }
}