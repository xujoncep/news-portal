using Microsoft.AspNetCore.Mvc;
using NewsPortal.Application.Services;
using NewsPortal.Core.DTOs;
using NewsPortal.Web.ViewModels;

namespace NewsPortal.Web.Controllers;

public class NewsController : Controller
{
    private readonly INewsService _newsService;
    private readonly ICategoryService _categoryService;
    private readonly INewsSourceService _sourceService;

    public NewsController(
        INewsService newsService,
        ICategoryService categoryService,
        INewsSourceService sourceService)
    {
        _newsService = newsService;
        _categoryService = categoryService;
        _sourceService = sourceService;
    }

    public async Task<IActionResult> Index(int page = 1)
    {
        var news = await _newsService.GetLatestNewsAsync(page, 20);

        var viewModel = new NewsListViewModel
        {
            News = news,
            Title = "Latest News"
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Detail(string slug)
    {
        var article = await _newsService.GetNewsDetailAsync(slug);
        if (article == null)
            return NotFound();

        var relatedNews = !string.IsNullOrEmpty(article.CategorySlug)
            ? (await _newsService.GetNewsByCategoryAsync(article.CategorySlug, 1, 5)).Items
            : await _newsService.GetFeaturedNewsAsync(5);

        var viewModel = new NewsDetailViewModel
        {
            Article = article,
            RelatedNews = relatedNews.Where(n => n.Id != article.Id).Take(4)
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Category(string slug, int page = 1)
    {
        var category = await _categoryService.GetCategoryBySlugAsync(slug);
        if (category == null)
            return NotFound();

        var news = await _newsService.GetNewsByCategoryAsync(slug, page, 20);

        var viewModel = new CategoryViewModel
        {
            Category = category,
            News = news
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Source(string slug, int page = 1)
    {
        var source = await _sourceService.GetSourceBySlugAsync(slug);
        if (source == null)
            return NotFound();

        var news = await _newsService.GetNewsBySourceAsync(slug, page, 20);

        var viewModel = new NewsListViewModel
        {
            News = news,
            Title = $"News from {source.Name}",
            SourceSlug = slug
        };

        return View("Index", viewModel);
    }

    public async Task<IActionResult> Search(string q, int page = 1)
    {
        if (string.IsNullOrWhiteSpace(q))
            return RedirectToAction(nameof(Index));

        var results = await _newsService.SearchNewsAsync(new SearchQueryDto
        {
            Query = q,
            Page = page,
            PageSize = 20
        });

        var viewModel = new SearchViewModel
        {
            Query = q,
            Results = results
        };

        return View(viewModel);
    }
}
