using TodoTasks.Domain.Enums;

namespace TodoTasks.Application.Common.DTOs;

/// <summary>
/// Data transfer object for category information.
/// </summary>
/// <remarks>
/// Used to transfer category data between the API layer and clients.
/// This immutable record ensures data consistency in API responses.
/// </remarks>
public record CategoryDto
{
    /// <summary>
    /// Gets the unique identifier of the category.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Gets the name of the category.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets the optional description of the category.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Gets the color associated with the category for visual identification.
    /// </summary>
    public TaskColorEnum? Color { get; init; }

    /// <summary>
    /// Gets the date and time when the category was created.
    /// </summary>
    public DateTime CreatedAt { get; init; }

    /// <summary>
    /// Gets the date and time when the category was last updated, or null if never updated.
    /// </summary>
    public DateTime? UpdatedAt { get; init; }
}
