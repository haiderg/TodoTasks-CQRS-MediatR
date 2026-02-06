using TodoTasks.Domain.ValueObjects;
using TodoTasks.Domain.Enums;

namespace TodoTasks.Domain.Entities;

/// <summary>
/// Represents a task category for organizing and grouping tasks.
/// </summary>
/// <remarks>
/// Categories provide a way to organize tasks by type or context (e.g., Work, Personal, Shopping).
/// Each category has a name, optional description, and color for visual identification.
/// </remarks>
public class Category : Entity
{
    /// <summary>
    /// Gets the name of the category.
    /// </summary>
    /// <remarks>
    /// The name is required and must be between 1 and 30 characters.
    /// </remarks>
    public string Name { get; private set; } = string.Empty;

    /// <summary>
    /// Gets the optional description of the category.
    /// </summary>
    public string? Description { get; private set; }

    /// <summary>
    /// Gets the color associated with the category for visual identification.
    /// </summary>
    public TaskColorEnum? Color { get; private set; }

    private Category() { } // For EF Core

    private Category(CreateCategoryRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ArgumentException("Category name cannot be empty", nameof(request.Name));
        
        if (request.Name.Length > 30)
            throw new ArgumentException("Category name cannot exceed 30 characters", nameof(request.Name));
        
        Name = request.Name.Trim();
        Description = request.Description?.Trim();
        Color = request.Color;
    }

    /// <summary>
    /// Updates the category with new information.
    /// </summary>
    /// <param name="request">The update request containing the new category data.</param>
    /// <remarks>
    /// Only properties specified in the request are updated. The UpdatedAt timestamp is automatically set to the current UTC time.
    /// </remarks>
    /// <exception cref="ArgumentException">Thrown when the name is empty or exceeds 30 characters.</exception>
    public void Update(UpdateCategoryRequest request)
    {
        if (request.HasName)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ArgumentException(message: "Category name cannot be empty", nameof(request.Name));
            
            if (request.Name.Length > 30)
                throw new ArgumentException(message: "Category name cannot exceed 30 characters", nameof(request.Name));

            Name = request.Name.Trim();
        }

        if (request.HasDescription)
            Description = request.Description?.Trim();

        if (request.HasColor)
            Color = request.Color!;

        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Creates a new category instance.
    /// </summary>
    /// <param name="request">The creation request containing the category data.</param>
    /// <returns>A new Category instance.</returns>
    /// <exception cref="ArgumentException">Thrown when the name is empty or exceeds 30 characters.</exception>
    public static Category Create(CreateCategoryRequest request)
    {
        return new Category(request);
    }
}
