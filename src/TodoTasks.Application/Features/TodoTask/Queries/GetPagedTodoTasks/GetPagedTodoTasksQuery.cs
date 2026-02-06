using MediatR;
using TodoTasks.Application.Common.DTOs;
using TodoTasks.Application.Common.Models;

namespace TodoTasks.Application.Features.TodoTask.Queries.GetPagedTodoTasks;

public record GetPagedTodoTasksQuery(int PageNumber = 1, int PageSize = 20) : IRequest<PagedResponse<TodoTaskDto>>;