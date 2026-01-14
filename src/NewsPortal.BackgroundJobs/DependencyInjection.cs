using Microsoft.Extensions.DependencyInjection;
using NewsPortal.BackgroundJobs.Jobs;

namespace NewsPortal.BackgroundJobs;

public static class DependencyInjection
{
    public static IServiceCollection AddBackgroundJobs(this IServiceCollection services)
    {
        services.AddScoped<INewsFetchJob, NewsFetchJob>();
        services.AddScoped<ICacheCleanupJob, CacheCleanupJob>();

        return services;
    }
}
