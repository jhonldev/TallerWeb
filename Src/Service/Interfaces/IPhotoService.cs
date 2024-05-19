using CloudinaryDotNet.Actions;

namespace TallerWeb.Src.Service.Interfaces
{
    public interface IPhotoService
    {
        public Task<ImageUploadResult> AddPhoto(IFormFile photo);

        public Task<DeletionResult> DeletePhoto(string publicId);
    }
}