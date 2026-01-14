namespace NewsPortal.Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    INewsArticleRepository NewsArticles { get; }
    ICategoryRepository Categories { get; }
    INewsSourceRepository NewsSources { get; }
    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}
