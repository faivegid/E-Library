using GBLAC.Models;
using GBLAC.Services.TokenGeneration.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GBLAC.Services.TokenGeneration.Implementation
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        public TokenGenerator(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<string> CreateTokenAsync(AppUser appUser)
        {
            var issuerSigningKey = EncodeSecret();
            var appUserClaims = GenerateClaims(appUser);
            await AddRolesAsync(appUser, appUserClaims);
            var token = GenerateToken(appUserClaims, issuerSigningKey);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #region EncodeSecret
        private SymmetricSecurityKey EncodeSecret()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
        }
        #endregion

        #region GenerateToken
        private JwtSecurityToken GenerateToken(List<Claim> appUserClaims, SymmetricSecurityKey issuerSigningKey)
        {
            return new JwtSecurityToken
                (
                    audience: _configuration["JWTSettings:Audience"],
                    issuer: _configuration["JWTSettings:Issuer"],
                    claims: appUserClaims,
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: new SigningCredentials(issuerSigningKey, SecurityAlgorithms.HmacSha256)
                );
        }
        #endregion

        #region AddRolesAsync
        private async Task AddRolesAsync(AppUser appUser, List<Claim> appUserClaims)
        {
            var roles = await _userManager.GetRolesAsync(appUser);

            foreach (var role in roles)
            {
                appUserClaims.Add(new Claim(ClaimTypes.Role, role));
            }
        }
        #endregion

        #region GenerateClaims
        private static List<Claim> GenerateClaims(AppUser appUser)
        {
            return new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,appUser.Id),
                new Claim(ClaimTypes.Email,appUser.Email)
            };
        }
        #endregion
    }
}
