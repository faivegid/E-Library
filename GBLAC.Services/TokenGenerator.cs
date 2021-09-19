using GBLAC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GBLAC.Services
{
    public class TokenGenerator
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        public TokenGenerator(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<string> GenerateTokenAsync(AppUser appUser)
        {
           
            var appUserClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,appUser.Id),
                new Claim(ClaimTypes.Email,appUser.Email)
            };

            var roles = await _userManager.GetRolesAsync(appUser);

            foreach (var role in roles)
            {
                appUserClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSettings:SecretKey"]));

            var token = new JwtSecurityToken
                (
                    audience: _configuration["JWTSettings:Audience"],
                    issuer: _configuration["JWTSettings:Issuer"],
                    claims: appUserClaims,
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: new SigningCredentials(issuerSigningKey,SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
