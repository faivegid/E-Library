using AutoMapper;
using CloudinaryDotNet.Actions;
using GBLAC.Models;
using GBLAC.Models.DTOs;
using GBLAC.Services.APIServices.Interfaces;
using GBLAC.Services.TokenGeneration.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GBLAC.Services.APIServices.Implementations
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUploadImageService _ImageUploader;
        private readonly IMapper _mapper;
        private readonly ITokenGenerator _tokenGenerator;


        public UserService(
            ILogger<UserService> logger,
            UserManager<AppUser> userManager,
            IUploadImageService ImageUploader,
            IMapper mapper,
            ITokenGenerator tokenGenerator)
        {
            _logger = logger;
            _userManager = userManager;
            _ImageUploader = ImageUploader;
            _mapper = mapper;
            _tokenGenerator = tokenGenerator;
        }

        #region User Login
        public async Task<UserDTO> LoginUserAsync(LoginDTO login)
        {
            var user = await ValidateUserRequestAsync(login);
            return await GetUserDTOWithToken(user);
        }

        private async Task<UserDTO> GetUserDTOWithToken(AppUser user)
        {
            var userDTO = _mapper.Map<UserDTO>(user);
            userDTO.Token = await _tokenGenerator.CreateTokenAsync(user);
            return userDTO;
        }
        private async Task<AppUser> ValidateUserRequestAsync(LoginDTO login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user != null)
            {
                var result = await _userManager.CheckPasswordAsync(user, login.Password);
                if (result) return user;
            }
            throw new BadHttpRequestException("Invalid Login Credentials", StatusCodes.Status400BadRequest);
        }
        #endregion
        #region User Registration
        public async Task<bool> RegisterUserAsync(RegisterationDTO request)
        {
            _logger.LogInformation($"Registration request by {request.Email}");
            return await AddUserAsync(_mapper.Map<AppUser>(request), request.Password);
        }
        public async Task<bool> AddUserAsync(AppUser user, string password)
        {
            var createResult = await _userManager.CreateAsync(user, password);
            if (createResult.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(user, "admin");
                if (roleResult.Succeeded)
                {
                    _logger.LogInformation($"Success added {user.Email} with Customer role to database");
                    return true;
                }
                await _userManager.DeleteAsync(user);
                throw new BadHttpRequestException("Internal Server Error", StatusCodes.Status500InternalServerError);
            }
            string errors = GetErrorFromIdentiy(createResult);
            throw new BadHttpRequestException(errors, StatusCodes.Status500InternalServerError);
        }

        private static string GetErrorFromIdentiy(IdentityResult createResult)
        {
            string errors = "";
            createResult.Errors.ToList()
                .ForEach(e => errors += e.Description);
            return errors;
        }
        #endregion

        public IList<AppUser> GetUserAsync(Expression<Func<AppUser, bool>> expression = null)
        {
            return expression == null ? _userManager.Users.ToList()
                                      : _userManager.Users.Where(expression).ToList();
        }
        public async Task<IdentityResult> UpdateUserPasswordAsync(
            AppUser user, string oldPassword, string newPassword) =>
            await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        public async Task<IdentityResult> UpdateUserAsync(AppUser user) => await _userManager.UpdateAsync(user);
        public async Task<IdentityResult> DeleteUserAsync(AppUser user) => await _userManager.DeleteAsync(user);
        public async Task<ImageUploadResult> UploadIProfileImageAsync(IFormFile image) => await _ImageUploader.UploadImageAsync(image);
    }
}
