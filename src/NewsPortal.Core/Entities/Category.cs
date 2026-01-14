namespace NewsPortal.Core.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string NameBn { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Icon { get; set; }
    public string? Color { get; set; }
    public int SortOrder { get; set; }

    // Navigation
    public virtual ICollection<NewsArticle> Articles { get; set; } = new List<NewsArticle>();
}
