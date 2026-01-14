using NewsPortal.Core.Enums;

namespace NewsPortal.Core.Entities;

public class NewsSource : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;
    public string? LogoUrl { get; set; }
    public FetchMethod FetchMethod { get; set; } = FetchMethod.Rss;
    public string? RssFeedUrl { get; set; }
    public string? ApiEndpoint { get; set; }
    public string? ApiKey { get; set; }
    public int FetchIntervalMinutes { get; set; } = 30;
    public DateTime? LastFetchedAt { get; set; }

    // Navigation
    public virtual ScrapingConfig? ScrapingConfig { get; set; }
    public virtual ICollection<NewsArticle> Articles { get; set; } = new List<NewsArticle>();
}
