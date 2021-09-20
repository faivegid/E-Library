using CloudinaryDotNet.Actions;
using GBLAC.Models;
using GBLAC.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GBLAC.Services.APIServices.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> LoginUserAsync(LoginDTO login);
        Task<UserDTO> RegisterUserAsync(RegisterationDTO request);
        Task<IdentityResult> AddUserAsync(AppUser user, string password);
        Task<IdentityResult> DeleteUserAsync(AppUser user);
        IList<AppUser> GetAllUserAsync(Expression<Func<AppUser, bool>> expression = null);
        Task<IdentityResult> UpdateUserAsync(AppUser user);
        Task<IdentityResult> UpdateUserPasswordAsync(AppUser user, string oldPassword, string newPassword);
        Task<ImageUploadResult> UploadIProfileImageAsync(IFormFile image);
    }
}