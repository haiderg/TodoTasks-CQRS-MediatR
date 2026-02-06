using FluentValidation;
using TodoTasks.Application.Features.Category.Queries.GetAllCategories;

namespace TodoTasks.Application.Features.Category.Queries.GetPagedCategories;

public class GetPagedCategoriesQueryValidator : AbstractValidator<GetPagedCategoriesQuery>
{
    public GetPagedCategoriesQueryValidator()
    {
        RuleFor(x => x.PageNumber).GreaterThan(0).WithMessage("Page number must be greater than 0");
        RuleFor(x => x.PageSize).GreaterThan(0).WithMessage("Page size must be greater than 0").LessThan(1000).WithMessage("Page size must be less than 1000");
    }
}