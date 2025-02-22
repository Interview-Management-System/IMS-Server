using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace InterviewManagementSystem.Application.Services
{
    public interface ICloudinaryService
    {
        Task<ImageUploadResult?> UploadFileAsync(IFormFile? file);
    }
}
