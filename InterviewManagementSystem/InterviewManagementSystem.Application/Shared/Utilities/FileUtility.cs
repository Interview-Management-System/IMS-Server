using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InterviewManagementSystem.Application.Shared.Utilities;

public static class FileUtility
{

    private const long MaxFileSize = 1 * 1024 * 1024;

    public static async Task<byte[]> ConvertFileToBytes(IFormFile? file)
    {
        if (file == null)
            return [];

        ApplicationException.ThrowIfInvalidOperation(file.Length > MaxFileSize, "The file size exceeded its limit (1MB)");

        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);

        return memoryStream.ToArray();
    }


    public static FileContentResult CreateFileContentResultFromBytes(byte[]? bytes)
    {
        var fileContentResult = new FileContentResult(bytes ?? [], "application/pdf") { FileDownloadName = "download" };
        return fileContentResult;
    }
}
