using Microsoft.Extensions.DependencyInjection;
using NewsPortal.Application.Services;
using NewsPortal.Core.Interfaces;

namespace NewsPortal.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<INewsService, NewsService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<INewsSourceService, NewsSourceService>();
        services.AddScoped<IRssFeedService, RssFeedService>();
        services.AddScoped<INewsFetcherService, NewsFetcherService>();
        services.AddHttpClient<IScrapingService, ScrapingService>();

        return services;
    }
}
