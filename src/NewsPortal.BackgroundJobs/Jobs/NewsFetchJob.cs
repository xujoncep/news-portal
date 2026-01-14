using Microsoft.Extensions.Logging;
using NewsPortal.Application.Services;
using NewsPortal.Core.Enums;
using NewsPortal.Core.Interfaces;

namespace NewsPortal.BackgroundJobs.Jobs;

public interface INewsFetchJob
{
    Task FetchAllSourcesAsync();
    Task FetchSourceAsync(int sourceId);
}

public class NewsFetchJob : INewsFetchJob
{
    private readonly INewsSourceService _sourceService;
    private readonly INewsFetcherService _fetcherService;
    private readonly INewsService _newsService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<NewsFetchJob> _logger;

    public NewsFetchJob(
        INewsSourceService sourceService,
        INewsFetcherService fetcherService,
        INewsService newsService,
        IUnitOfWork unitOfWork,
        ILogger<NewsFetchJob> logger)
    {
        _sourceService = sourceService;
        _fetcherService = fetcherService;
        _newsService = newsService;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task FetchAllSourcesAsync()
    {
        _logger.LogInformation("Starting to fetch news from all sources");

        var sources = await _sourceService.GetActiveSourcesForFetchingAsync();

        foreach (var source in sources)
        {
            try
            {
                // Check if it's time to fetch
                if (source.LastFetchedAt.HasValue)
                {
                    var timeSinceLastFetch = DateTime.UtcNow - source.LastFetchedAt.Value;
                    if (timeSinceLastFetch.TotalMinutes < source.FetchIntervalMinutes)
                    {
                        _logger.LogDebug("Skipping source {SourceName} - not yet time to fetch", source.Name);
                        continue;
                    }
                }

                await FetchSourceAsync(source.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching news from source: {SourceName}", source.Name);
            }
        }

        _logger.LogInformation("Completed fetching news from all sources");
    }

    public async Task FetchSourceAsync(int sourceId)
    {
        var sources = await _sourceService.GetActiveSourcesForFetchingAsync();
        var source = sources.FirstOrDefault(s => s.Id == sourceId);

        if (source == null)
        {
            _logger.LogWarning("Source not found: {SourceId}", sourceId);
            return;
        }

        _logger.LogInformation("Fetching news from source: {SourceName}", source.Name);

        try
        {
            var articles = source.FetchMethod switch
            {
                FetchMethod.Rss => await _fetcherService.FetchFromRssAsync(source),
                FetchMethod.Api => await _fetcherService.FetchFromApiAsync(source),
                FetchMethod.Scrape => await _fetcherService.FetchByScrapingAsync(source),
                _ => throw new NotSupportedException($"Fetch method not supported: {source.FetchMethod}")
            };

            var importedCount = await _newsService.ImportNewsArticlesAsync(articles);

            // Update last fetched time
            await _unitOfWork.NewsSources.UpdateLastFetchedAsync(sourceId);

            _logger.LogInformation("Imported {Count} articles from {SourceName}", importedCount, source.Name);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch news from source: {SourceName}", source.Name);
            throw;
        }
    }
}
