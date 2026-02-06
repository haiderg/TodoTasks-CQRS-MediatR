namespace TodoTasks.Domain.ValueObjects;

public record PagedResult<T>
{
    public IEnumerable<T> Items { get; init; } = [];
    public int TotalCount { get; init; }
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    internal bool HasNextPage => PageNumber < TotalPages;
    internal bool HasPreviousPage => PageNumber > 1;
}