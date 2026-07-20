namespace OrderManagement.Application.Common;

public class PagedResult<T>
{
    public IReadOnlyCollection<T> Items { get; init; } = [];

    public int TotalItems { get; init; }

    public int Page { get; init; }

    public int PageSize { get; init; }

    public int TotalPages =>
        (int)Math.Ceiling(TotalItems / (double)PageSize);

    public bool HasPreviousPage => Page > 1;

    public bool HasNextPage => Page < TotalPages;
}