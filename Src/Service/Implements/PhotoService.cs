using TallerWeb.Src.Service.Interfaces;
using CloudinaryDotNet;
using Microsoft.Extensions.Options;
using TallerWeb.Src.Helpers;
using CloudinaryDotNet.Actions;

namespace TallerWeb.Src.Service.Implements{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary; 
        public PhotoService(IOptions<CloudinarySettings> cloudinaryConfig){

            var acc = new Account(
                cloudinaryConfig.Value.CloudName,
                cloudinaryConfig.Value.ApiKey,
                cloudinaryConfig.Value.ApiSecret
            );
            _cloudinary = new Cloudinary(acc);
        }

        public async Task<ImageUploadResult> AddPhoto(IFormFile photo){

            var uploadResult = new ImageUploadResult();
            if(photo.Length > 0){
                using var stream = photo.OpenReadStream();
                var uploadParams = new ImageUploadParams{
                    File = new FileDescription(photo.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face"),Folder = "TallerWeb" 
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
            return uploadResult;
        }

        public async Task<DeletionResult> DeletePhoto(string publicId){
            var deleteParams = new DeletionParams(publicId);
        return await _cloudinary.DestroyAsync(deleteParams);
    }
    }
}