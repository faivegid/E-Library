using GBLAC.Data;
using GBLAC.Models;
using GBLAC.Models.AutoMapper;
using GBLAC.Models.DTOs;
using GBLAC.Repository.APIUnitOfWork.Implementaiton;
using GBLAC.Repository.APIUnitOfWork.Interfaces;
using GBLAC.Services.APIServices.Implementations;
using GBLAC.Services.APIServices.Interfaces;
using GBLAC.Services.TokenGeneration.Implementation;
using GBLAC.Services.TokenGeneration.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace GBLAC.MVC
{
    public static class APIStartupExtension
    {
        public static void ConfigureAPIDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureAPIDatabase(configuration); //Configure DBContext

            services.ConfigureAPIIdentity(); //COnfigure Identity
            services.ConfigureAPICloudinary(configuration); //Configure Cloudinary
            services.AddAutoMapper(typeof(MappingSetup));


            services.ConfigureAPIInterfaces(); //All Dependency injection of Interfaces created

            services.ConfigureAPIJWTAuthentication(configuration); // Configure JWT settings

        }
        public static void ConfigureAPIInterfaces(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ITokenGenerator, TokenGenerator>();
            services.AddTransient<IUploadImageService, UploadImageService>();
            services.AddTransient<IUserService, UserService>();
        }
        public static void ConfigureAPIDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<GBlacContext>(options => options.UseSqlite(configuration.GetConnectionString("DefaultSettings")));
        }
        public static void ConfigureAPICloudinary(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ImageSettingDTO>(configuration.GetSection("Cloudingary"));
        }

        public static void ConfigureAPIIdentity(this IServiceCollection services)
        {
            services
                .AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<GBlacContext>()
                .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 9;
            });
        }

        public static void ConfigureSession(this IServiceCollection services)
        {
            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);//We set Time here 
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
        }
        public static void ConfigureAPIJWTAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var key = configuration.GetSection("JWT:SecretKey").Value;
            //var key = Environment.GetEnvironmentVariable("JWTKey"); 

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }
            ).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidAudience = configuration.GetSection("JWT:Audience").Value,
                    ValidIssuer = configuration.GetSection("JWT:Issuer").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                };
            });
        }
    }
}
