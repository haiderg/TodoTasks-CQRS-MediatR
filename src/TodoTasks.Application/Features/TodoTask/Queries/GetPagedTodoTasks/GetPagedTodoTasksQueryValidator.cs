using FluentValidation;
using FluentValidation.Validators;

namespace TodoTasks.Application.Features.TodoTask.Queries.GetPagedTodoTasks;

public class GetPagedTodoTasksQueryValidator : AbstractValidator<GetPagedTodoTasksQuery>
{
    public GetPagedTodoTasksQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0).WithMessage("Page number must be greater than 0");

        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage("Page size must be greater than 0").LessThanOrEqualTo(1000).WithMessage("Max page size is 1000");

    }

}

