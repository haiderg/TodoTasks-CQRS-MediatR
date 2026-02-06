using FluentValidation;

namespace TodoTasks.Application.Features.TodoTask.Queries.GetTodoTaskById;

public class GetTodoTaskByIdQueryValidator : AbstractValidator<GetTodoTaskByIdQuery>
{
    public GetTodoTaskByIdQueryValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be greater than 0");
    }
}