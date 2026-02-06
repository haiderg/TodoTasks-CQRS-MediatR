using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoTasks.Application.Common.DTOs;
using TodoTasks.Application.Features.TodoTask.Commands.CreateTodoTask;
using TodoTasks.Application.Features.TodoTask.Queries.GetPagedTodoTasks;
using TodoTasks.Application.Common.Models;
using TodoTasks.Application.Features.TodoTask.Queries.GetTodoTaskById;
using TodoTasks.Application.Features.TodoTask.Commands.UpdateTodoTask;
using TodoTasks.Application.Features.TodoTask.Commands.DeleteTodoTask;


namespace TodoTasks.API.Controllers;

[ApiController]
//[Authorize]
[Route("api/[controller]")]
public class TodoTasksController : ControllerBase
{
    private readonly IMediator _mediator;
    public TodoTasksController( IMediator mediator)
    {
        _mediator = mediator;
    }
          
    [HttpGet("paged")]
    public async Task<ActionResult<PagedResponse<TodoTaskDto>>> GetPagedTodoTasks([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetPagedTodoTasksQuery(pageNumber, pageSize), cancellationToken);
        return Ok(result);
    }
       
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TodoTaskDto>> GetTodoTask(int id, CancellationToken cancellationToken)
    {
        var task = await _mediator.Send(new GetTodoTaskByIdQuery(id), cancellationToken);
        return Ok(task);
    }

    [HttpPost]
    public async Task<ActionResult<TodoTaskDto>> CreateTodoTask([FromBody] CreateTodoTaskCommand command, CancellationToken cancellationToken)
    {
        var task = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetTodoTask), new { id = task.Id }, task);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateTodoTask([FromRoute] int id, [FromBody] UpdateTodoTaskCommand command, CancellationToken cancellationToken)
    {
        command = command with { Id = id };
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTodoTask(int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteTodoTaskCommand(id), cancellationToken);
        return NoContent();
    }
       
}