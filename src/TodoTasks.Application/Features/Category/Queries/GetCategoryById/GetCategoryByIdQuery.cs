using MediatR;
using TodoTasks.Application.Common.DTOs;

namespace TodoTasks.Application.Features.Category.Queries.GetCategoryById;

public record GetCategoryByIdQuery(int Id) :IRequest<CategoryDto>;

  