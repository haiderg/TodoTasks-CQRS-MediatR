using FluentValidation;

namespace TodoTasks.Application.Features.TodoTask.Commands.DeleteTodoTask;

public class DeleteTodoTaskCommandValidator : AbstractValidator<DeleteTodoTaskCommand>
{
    public DeleteTodoTaskCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be greater than 0");
    }
}