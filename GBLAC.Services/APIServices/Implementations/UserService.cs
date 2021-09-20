using AutoMapper;
using CloudinaryDotNet.Actions;
using GBLAC.Models;
using GBLAC.Models.DTOs;
using GBLAC.Services.APIServices.Interfaces;
using GBLAC.Services.TokenGeneration.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<UserDTO> LoginUserAsync(LoginDTO login)
        {
            _logger.LogInformation($"Login request by {login.Email}");
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user != null)
            {
                var result = await _userManager.CheckPasswordAsync(user, login.Password);
                if (result)
                {
                    var userDTO = _mapper.Map<UserDTO>(user);
                    userDTO.Token = await _tokenGenerator.CreateTokenAsync(user);
                    _logger.LogInformation($"Successful login by {login.Email}");
                    return userDTO;
                }
            }

            throw new BadHttpRequestException("Invalid Login Credentials", StatusCodes.Status400BadRequest);
        }
        public async Task<UserDTO> RegisterUserAsync(RegisterationDTO request)
        {
            _logger.LogInformation($"Registration request by {request.Email}");
            var user = _mapper.Map<AppUser>(request);
            var result = await AddUserAsync(user, request.Password);            
            if (result.Succeeded)
            {
                _logger.LogInformation($"Registration Sucessfull by {request.Email}");
                return _mapper.Map<UserDTO>(user);
            }
            throw new BadHttpRequestException("Error Occurrred Creating User", StatusCodes.Status400BadRequest);
        }
        public async Task<IdentityResult> AddUserAsync(AppUser user, string password)
        {
            var createResult = await _userManager.CreateAsync(user, password);
            if (createResult.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(user, "admin");
                if (roleResult.Succeeded)
                {
                    _logger.LogInformation($"Success added {user.Email} with Customer role to database");
                    return roleResult;
                }
                await _userManager.DeleteAsync(user);
                return roleResult;
            }
            return createResult;
        }
        public IList<AppUser> GetAllUserAsync(Expression<Func<AppUser, bool>> expression = null)
        {
            var users = _userManager.Users.AsQueryable();
            if (expression != null)
            {
                users = users.Where(expression);
            }
            return users.ToList();
        }
        public async Task<IdentityResult> UpdateUserPasswordAsync(
            AppUser user, string oldPassword, string newPassword) =>
            await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        public async Task<IdentityResult> UpdateUserAsync(AppUser user) => await _userManager.UpdateAsync(user);
        public async Task<IdentityResult> DeleteUserAsync(AppUser user) => await _userManager.DeleteAsync(user);
        public async Task<ImageUploadResult> UploadIProfileImageAsync(IFormFile image) => await _ImageUploader.UploadImageAsync(image);
    }
}
