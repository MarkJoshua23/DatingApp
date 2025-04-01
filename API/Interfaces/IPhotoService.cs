using System;
using CloudinaryDotNet.Actions;

namespace API.Interfaces;

public interface IPhotoService
{
    //add photo
    Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
    //delete photo
    Task<DeletionResult> DeletePhotoAsync(string publicId);
}
