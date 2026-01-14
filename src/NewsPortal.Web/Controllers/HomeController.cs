using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Application.Services;
using NewsPortal.Web.Models;
using NewsPortal.Web.ViewModels;

namespace NewsPortal.Web.Controllers;

public class HomeController : Controller
{
    private readonly INewsService _newsService;
    private readonly ICategoryService _categoryService;
    private readonly ILogger<HomeController> _logger;

    public HomeController(
        INewsService newsService,
        ICategoryService categoryService,
        ILogger<HomeController> logger)
    {
        _newsService = newsService;
        _categoryService = categoryService;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var viewModel = new HomeViewModel
        {
            FeaturedNews = await _newsService.GetFeaturedNewsAsync(5),
            LatestNews = (await _newsService.GetLatestNewsAsync(1, 12)).Items,
            Categories = await _categoryService.GetAllCategoriesAsync()
        };

        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
