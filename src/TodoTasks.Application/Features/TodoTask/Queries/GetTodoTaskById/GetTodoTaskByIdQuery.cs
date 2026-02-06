using MediatR;
using TodoTasks.Application.Common.DTOs;

namespace TodoTasks.Application.Features.TodoTask.Queries.GetTodoTaskById;

public record GetTodoTaskByIdQuery(int Id) : IRequest<TodoTaskDto>;

