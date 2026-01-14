using Microsoft.AspNetCore.Mvc;
using NewsPortal.Application.Services;
using NewsPortal.Core.DTOs;
using NewsPortal.Core.Interfaces;

namespace NewsPortal.Web.Controllers;

[Route("api")]
[ApiController]
public class ApiController : ControllerBase
{
    private readonly INewsService _newsService;
    private readonly ICategoryService _categoryService;
    private readonly IImageStorageService _imageStorage;

    public ApiController(
        INewsService newsService,
        ICategoryService categoryService,
        IImageStorageService imageStorage)
    {
        _newsService = newsService;
        _categoryService = categoryService;
        _imageStorage = imageStorage;
    }

    [HttpGet("news")]
    public async Task<ActionResult<PagedResultDto<NewsArticleListDto>>> GetNews(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var result = await _newsService.GetLatestNewsAsync(page, pageSize);
        return Ok(result);
    }

    [HttpGet("news/{slug}")]
    public async Task<ActionResult<NewsArticleDto>> GetNewsDetail(string slug)
    {
        var article = await _newsService.GetNewsDetailAsync(slug);
        if (article == null)
            return NotFound();

        return Ok(article);
    }

    [HttpGet("categories")]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        return Ok(categories);
    }

    [HttpGet("categories/{slug}/news")]
    public async Task<ActionResult<PagedResultDto<NewsArticleListDto>>> GetNewsByCategory(
        string slug,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var result = await _newsService.GetNewsByCategoryAsync(slug, page, pageSize);
        return Ok(result);
    }

    [HttpGet("search")]
    public async Task<ActionResult<PagedResultDto<NewsArticleListDto>>> SearchNews(
        [FromQuery] string q,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var result = await _newsService.SearchNewsAsync(new SearchQueryDto
        {
            Query = q,
            Page = page,
            PageSize = pageSize
        });

        return Ok(result);
    }

    [HttpGet("images/{id}")]
    public async Task<IActionResult> GetImage(string id)
    {
        var result = await _imageStorage.GetImageAsync(id);
        if (result == null)
            return NotFound();

        return File(result.Value.Data, result.Value.ContentType);
    }
}
