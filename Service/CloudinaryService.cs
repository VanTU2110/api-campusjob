using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

public class CloudinaryService
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryService(IConfiguration configuration)
    {
        var account = new Account(
            configuration["Cloudinary:CloudName"],
            configuration["Cloudinary:ApiKey"],
            configuration["Cloudinary:ApiSecret"]
        );
        _cloudinary = new Cloudinary(account);
    }
    public class CloudinaryUploadResult
    {
        public string PublicId { get; set; }
        public string Url { get; set; }
        
    }


    public async Task<CloudinaryUploadResult> UploadFileAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("File cannot be null or empty");

        using var stream = file.OpenReadStream();
        var uploadParams = new RawUploadParams
        {
            File = new FileDescription(file.FileName, stream),
            PublicId = Path.GetFileNameWithoutExtension(file.FileName),
            Overwrite = true,
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);

        return new CloudinaryUploadResult
        {
            PublicId = uploadResult.PublicId,
            Url = uploadResult.Url?.ToString(),
        };
    }
    public async Task<string> DeleteFileAsync(string publicId)
    {
        var deleteParams = new DeletionParams(publicId);
        var result = await _cloudinary.DestroyAsync(deleteParams);

        return result.Result == "OK" ? "File deleted successfully" : "Failed to delete file";
    }
}
