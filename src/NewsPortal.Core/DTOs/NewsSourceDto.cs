using NewsPortal.Core.Enums;

namespace NewsPortal.Core.DTOs;

public class NewsSourceDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;
    public string? LogoUrl { get; set; }
    public FetchMethod FetchMethod { get; set; }
    public string? RssFeedUrl { get; set; }
    public bool IsActive { get; set; }
    public DateTime? LastFetchedAt { get; set; }
    public int ArticleCount { get; set; }
}

public class CreateNewsSourceDto
{
    public string Name { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;
    public string? LogoUrl { get; set; }
    public FetchMethod FetchMethod { get; set; }
    public string? RssFeedUrl { get; set; }
    public string? ApiEndpoint { get; set; }
    public string? ApiKey { get; set; }
    public int FetchIntervalMinutes { get; set; } = 30;
}
