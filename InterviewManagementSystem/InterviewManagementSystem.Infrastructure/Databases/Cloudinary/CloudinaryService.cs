using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using ApplicationException = InterviewManagementSystem.Application.Shared.Exceptions.ApplicationException;

namespace InterviewManagementSystem.Infrastructure.Databases.Cloudinary
{
    public sealed class CloudinaryService
    {

        private readonly CloudinaryDotNet.Cloudinary _cloudinary;


        public CloudinaryService(IOptions<CloudinarySetting> options)
        {

            var cloudSetting = options.Value;

            var account = new Account(cloudSetting.CloudName, cloudSetting.ApiKey, cloudSetting.ApiSecret);
            _cloudinary ??= new CloudinaryDotNet.Cloudinary(account);
        }


        public async Task<ImageUploadResult?> UploadFileAsync(IFormFile? file)
        {

            if (file != null && file.Length > 0)
            {
                using var stream = file.OpenReadStream();

                var uploadParam = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream)
                };


                var result = await _cloudinary.UploadAsync(uploadParam);
                ApplicationException.ThrowIfOperationFail(result.Error == null, "Upload/Process file failed");

                return result;
            }

            return null;
        }
    }
}
