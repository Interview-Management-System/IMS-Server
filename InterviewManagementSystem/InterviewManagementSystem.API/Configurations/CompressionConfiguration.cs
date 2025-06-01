using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;

namespace InterviewManagementSystem.API.Configurations;

internal static class CompressionConfiguration
{
    internal static void AddCompression(this IServiceCollection services)
    {
        services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true; // Enable compression for HTTPS requests
            options.Providers.Add<GzipCompressionProvider>();
            options.Providers.Add<BrotliCompressionProvider>();
            options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(["application/json"]);
            // Security checks for specific mime types if needed
        });

        services.Configure<GzipCompressionProviderOptions>(options =>
        {
            options.Level = CompressionLevel.Fastest; // Choose Fastest or Optimal
        });

        services.Configure<BrotliCompressionProviderOptions>(options =>
        {
            options.Level = CompressionLevel.Fastest; // Choose Fastest or Optimal
        });
    }
}
