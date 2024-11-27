namespace TodoApi.Commons.Models;

public sealed class PagedResult<T>
{
    public PagedResult(IEnumerable<T> results, int pageIndex, int pageSize, long count = 0)
    {
        Results = results ?? Enumerable.Empty<T>();
        TotalCount = count <= 0 ? Results.Count() : count;
        PageIndex = pageIndex;
        PageSize = pageSize;
    }

    public int PageIndex { get; }
    public int PageSize { get; }
    public long TotalCount { get; }
    public IEnumerable<T> Results { get; }
}

public static class PagedResultExtensions
{
    public static PagedResult<T> ToPagedResult<T>(this IEnumerable<T> result, int pageIndex, int pageSize, long count = 0)
        where T : class
    {
        return new PagedResult<T>(result, pageIndex, pageSize, count);
    }
}