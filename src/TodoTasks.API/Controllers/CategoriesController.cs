using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using TodoTasks.Application.Common.DTOs;
using MediatR;
using TodoTasks.Application.Features.Category.Queries.GetAllCategories;
using TodoTasks.Application.Common.Models;
using TodoTasks.Application.Features.Category.Queries.GetCategoryById;
using TodoTasks.Application.Features.Category.Commands.CreateCategory;
using TodoTasks.Application.Features.Category.Commands.DeleteCategory;
using TodoTasks.Application.Features.Category.Commands.UpdateCategory;

[ApiController]
//[Authorize]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;
    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResponse<CategoryDto>>> GetAllAsync(
        [FromQuery][Range(1, int.MaxValue)] int pageNumber = 1,
        [FromQuery][Range(1, 100)] int pageSize = 20,
        CancellationToken cancellationToken = default)
    {
        var categories = await _mediator.Send(new GetPagedCategoriesQuery(pageNumber,pageSize), cancellationToken);
        return Ok(categories);
    }

    [HttpGet("{id:int}", Name = "GetCategoryById")]
    public async Task<ActionResult<CategoryDto>> GetCategoryAsync(int id, CancellationToken cancellationToken = default)
    {
        var category = await _mediator.Send(new GetCategoryByIdQuery(id), cancellationToken);
        return Ok(category);
    }
    
    [HttpPost]
    public async Task<ActionResult<CategoryDto>> CreateCategoryAsync([FromBody] CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = await _mediator.Send(command, cancellationToken);
        return CreatedAtRoute("GetCategoryById", new { id = category.Id }, category);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCategoryAsync(int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteCategoryCommand(id), cancellationToken);
        return NoContent();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateCategoryAsync(int id, [FromBody] UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        command = command with { Id = id };
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
}