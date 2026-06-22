using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horyzonty_Nauki.Application.Attachments
{
    public interface IFileStorageService
    {
        Task<StoredFile> SaveAsync(IFormFile file, CancellationToken ct);

        Task DeleteAsync(string filePath, CancellationToken ct);
    }
}
