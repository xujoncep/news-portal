using NewsPortal.Core.DTOs;

namespace NewsPortal.Web.ViewModels;

public class HomeViewModel
{
    public IEnumerable<NewsArticleListDto> FeaturedNews { get; set; } = new List<NewsArticleListDto>();
    public IEnumerable<NewsArticleListDto> LatestNews { get; set; } = new List<NewsArticleListDto>();
    public IEnumerable<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
}

public class NewsListViewModel
{
    public PagedResultDto<NewsArticleListDto> News { get; set; } = new();
    public string? Title { get; set; }
    public string? CategorySlug { get; set; }
    public string? SourceSlug { get; set; }
    public string? SearchQuery { get; set; }
}

public class NewsDetailViewModel
{
    public NewsArticleDto Article { get; set; } = new();
    public IEnumerable<NewsArticleListDto> RelatedNews { get; set; } = new List<NewsArticleListDto>();
}

public class CategoryViewModel
{
    public CategoryDto Category { get; set; } = new();
    public PagedResultDto<NewsArticleListDto> News { get; set; } = new();
}

public class SearchViewModel
{
    public string Query { get; set; } = string.Empty;
    public PagedResultDto<NewsArticleListDto> Results { get; set; } = new();
}
