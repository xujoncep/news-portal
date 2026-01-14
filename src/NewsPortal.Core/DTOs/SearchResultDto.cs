namespace NewsPortal.Core.DTOs;

public class SearchResultDto
{
    public string Title { get; set; } = string.Empty;
    public string? Summary { get; set; }
    public string Url { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public DateTime? PublishedAt { get; set; }
    public string? SourceName { get; set; }
}

public class PagedResultDto<T>
{
    public List<T> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    public bool HasNextPage => Page < TotalPages;
    public bool HasPreviousPage => Page > 1;
}

public class SearchQueryDto
{
    public string? Query { get; set; }
    public int? CategoryId { get; set; }
    public int? SourceId { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}
