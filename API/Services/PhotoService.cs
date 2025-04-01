using System;
using API.Helpers;
using API.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace API.Services;


public class PhotoService : IPhotoService
{
    private readonly Cloudinary _cloudinary;
    //cloudinary settings is the helper for the api keys in appsetting
    public PhotoService(IOptions<CloudinarySettings> config)
    {
        //this is from service setup for api keys in extension cloudinary settings
        var acc = new Account(config.Value.CloudName, config.Value.ApiKey, config.Value.ApiSecret);
        //now we can use _cloudinary everywhere to access functionality
        _cloudinary = new Cloudinary(acc);
    }
    public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
    {
        var uploadResult = new ImageUploadResult();
        //if theres file
        if (file.Length > 0)
        {
            using var stream = file.OpenReadStream();

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Transformation = new Transformation()
                .Height(500).Width(500).Height(500)
                //gravity means the crop will focus on the face
                .Crop("fill").Gravity("face"),
                Folder="da-net8"
            };

            uploadResult = await _cloudinary.UploadAsync(uploadParams);
            
        }
        
        return uploadResult;
    }

    public async Task<DeletionResult> DeletePhotoAsync(string publicId)
    {
        var deleteParams = new DeletionParams(publicId);
        return await _cloudinary.DestroyAsync(deleteParams);
    }
}
