using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace GBLAC.Services.APIServices.Interfaces
{
    public interface IUploadImageService
    {
        Task<ImageUploadResult> UploadImageAsync(IFormFile image);
    }
}