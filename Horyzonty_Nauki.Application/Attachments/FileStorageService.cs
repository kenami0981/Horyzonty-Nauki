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
        CancellationToken ct)
    {
        // 1. pełna ścieżka pliku
        var fullPath = Path.Combine(_basePath, file.FileName);

        // 2. zapis pliku na dysk
        await using var stream = new FileStream(
            fullPath,
            FileMode.Create,
            FileAccess.Write);

        await file.CopyToAsync(stream, ct);

        // 3. zwrócenie informacji o pliku
        return new StoredFile
        {
            FileName = file.FileName,
            FilePath = fullPath,
            ContentType = file.ContentType,
            FileSize = file.Length
        };
    }

    public Task DeleteAsync(string filePath, CancellationToken ct)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        return Task.CompletedTask;
    }
}
