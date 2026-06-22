using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace Horyzonty_Nauki.Application.Attachments;

public class FileStorageService : IFileStorageService
{
    private readonly string _basePath;

    public FileStorageService(string basePath)
    {
        _basePath = basePath;

        if (!Directory.Exists(_basePath))
            Directory.CreateDirectory(_basePath);
    }

    public async Task<StoredFile> SaveAsync(
        IFormFile file,
        string filePath,
        CancellationToken ct)
    {
        var fullPath = Path.Combine(_basePath, filePath);

        await using var stream = new FileStream(
            fullPath,
            FileMode.Create,
            FileAccess.Write);

        await file.CopyToAsync(stream, ct);

        return new StoredFile
        {
            FileName = file.FileName,
            FilePath = filePath,
            ContentType = file.ContentType,
            FileSize = file.Length
        };
    }

    public Task DeleteAsync(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        return Task.CompletedTask;
    }

    public Task<StoredFileResponse?> GetAsync(
        string filePath)
    {
        var fullPath =
            Path.Combine(_basePath, filePath);

        if (!File.Exists(fullPath))
        {
            return Task.FromResult<StoredFileResponse?>(null);
        }

        var stream = new FileStream(
            fullPath,
            FileMode.Open,
            FileAccess.Read,
            FileShare.Read);

        var result = new StoredFileResponse
        {
            Content = stream,
            FileName = Path.GetFileName(fullPath),
            ContentType = "application/pdf"
        };

        return Task.FromResult<StoredFileResponse?>(result);
    }
}
