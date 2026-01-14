namespace NewsPortal.Core.Entities;

public class ScrapingConfig : BaseEntity
{
    public int SourceId { get; set; }
    public string? ListPageUrl { get; set; }
    public string? ArticleLinkSelector { get; set; }
    public string? TitleSelector { get; set; }
    public string? ContentSelector { get; set; }
    public string? SummarySelector { get; set; }
    public string? ImageSelector { get; set; }
    public string? DateSelector { get; set; }
    public string? AuthorSelector { get; set; }

    // Navigation
    public virtual NewsSource Source { get; set; } = null!;
}
