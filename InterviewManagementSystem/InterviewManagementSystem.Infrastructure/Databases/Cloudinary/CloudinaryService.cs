using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using InterviewManagementSystem.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using ApplicationException = InterviewManagementSystem.Application.Shared.Exceptions.ApplicationException;

namespace InterviewManagementSystem.Infrastructure.Databases.Cloudinary;


public sealed class CloudinaryService : ICloudinaryService
{

    private readonly CloudinarySetting _cloudinarySetting;
    private readonly CloudinaryDotNet.Cloudinary _cloudinary;


    public CloudinaryService(IOptions<CloudinarySetting> options)
    {

        _cloudinarySetting = options.Value;

        var account = new Account(_cloudinarySetting.CloudName, _cloudinarySetting.ApiKey, _cloudinarySetting.ApiSecret);
        _cloudinary ??= new CloudinaryDotNet.Cloudinary(account);
    }


    public async Task<ImageUploadResult?> UploadFileAsync(IFormFile? file)
    {

        if (file != null && file.Length > 0)
        {
            using var stream = file.OpenReadStream();

            var uploadParam = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, stream),
                Folder = _cloudinarySetting.FolderName,
            };


            var result = await _cloudinary.UploadAsync(uploadParam);
            ApplicationException.ThrowIfOperationFail(result.Error == null, "Upload/Process file failed");

            return result;
        }

        return null;
    }
}
