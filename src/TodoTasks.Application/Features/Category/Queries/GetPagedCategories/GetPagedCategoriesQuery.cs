using MediatR;
using TodoTasks.Application.Common.DTOs;
using TodoTasks.Application.Common.Models;

namespace TodoTasks.Application.Features.Category.Queries.GetAllCategories;

public record GetPagedCategoriesQuery(int PageNumber = 1, int PageSize = 20) : IRequest<PagedResponse<CategoryDto>>;