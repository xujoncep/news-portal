namespace NewsPortal.Core.Interfaces;

public interface IImageStorageService
{
    Task<string> UploadImageAsync(byte[] imageData, string fileName, string contentType);
    Task<string> UploadImageFromUrlAsync(string imageUrl, int newsArticleId);
    Task<(byte[] Data, string ContentType)?> GetImageAsync(string imageId);
    Task<string?> GetThumbnailIdAsync(string imageId);
    Task DeleteImageAsync(string imageId);
    Task<string> GenerateThumbnailAsync(string imageId, int width, int height);
}
