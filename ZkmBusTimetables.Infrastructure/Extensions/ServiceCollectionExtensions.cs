
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Core.Interfaces;
using ZkmBusTimetables.Core.Models;
using ZkmBusTimetables.Infrastructure.Persistance;
using ZkmBusTimetables.Infrastructure.Repositories;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using ZkmBusTimetables.Infrastructure.Configurations;
using ZkmBusTimetables.Infrastructure.Utils.JwtTokenHandler;

namespace ZkmBusTimetables.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ZkmDbContext>(options =>
            {
                options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ZKMdb;Trusted_Connection=True;");

            });
            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/ ";
                options.User.RequireUniqueEmail = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
                .AddEntityFrameworkStores<ZkmDbContext>()
                .AddDefaultTokenProviders()
                .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ZkmDbContext, Guid>>()
                .AddRoleStore<RoleStore<ApplicationRole, ZkmDbContext, Guid>>();
            services.AddOptions();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                    var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
                    var issuer = configuration["Jwt:Issuer"];
                    var audience = configuration["Jwt:Audience"];

                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = signingCredentials.Key,

                        // Validate the JWT Issuer (iss) claim
                        ValidateIssuer = true,
                        ValidIssuer = issuer,
                        // Validate the JWT Audience (aud) claim
                        ValidateAudience = true,
                        ValidAudience = audience,
                        // Validate the token expiry
                        ValidateLifetime = true,
                        // If you want to allow a certain amount of clock drift, set that here:
                        ClockSkew = TimeSpan.Zero,
                        AudienceValidator = (audience, securityToken, validationParameters) =>
                        {
                            var jwtToken = securityToken as JsonWebToken;
                            if (jwtToken is null) return false;

                            var userFingerprintHash = jwtToken.Claims.FirstOrDefault(c => c.Type == "userFingerprint")?.Value;
                            if (userFingerprintHash is null) return false;

                            return audience.Any(audience => audience.Equals(validationParameters.ValidAudience, StringComparison.OrdinalIgnoreCase));
                        }

                    };
                    new JwtBearerEvents().OnAuthenticationFailed = (context) =>
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return Task.CompletedTask;
                    };
                    new JwtBearerEvents().OnTokenValidated = (context) =>
                    {
                        var jwtToken = context.SecurityToken as JsonWebToken;
                        if (jwtToken is null) return Task.CompletedTask;

                        var userFingerprintHash = jwtToken.Claims.FirstOrDefault(c => c.Type == "userFingerprint")?.Value;
                        if (userFingerprintHash is null) return Task.CompletedTask;

                        var jwtSettings = Options.Create(new JwtSettings());
                        if (userFingerprintHash != new JwtTokenHandler(jwtSettings, configuration).GenerateUserFingerprintHash(context.Request.Cookies["__Secure-Fgp"].Replace("__Secure-Fgp=", "", StringComparison.InvariantCultureIgnoreCase)))
                        {
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            return Task.CompletedTask;
                        }
                        return Task.CompletedTask;
                    };
                });
            


            services.AddAuthorizationCore(options =>
            {
                options.AddPolicy("AuthPolicy", policy => policy.RequireRole("Employee", "Admin"));
            });
        

            services.AddScoped<ILinesRepository, LinesRepository>();
            services.AddScoped<IBusStopsRepository, BusStopsRepository>();
            services.AddScoped<IAddressesRepository, AddressesRepository>();
            services.AddScoped<IVariantsRepository, VariantsRepository>();
            services.AddScoped<IDeparturesRepository, DeparturesRepository>();
            services.AddScoped<IJwtTokenHandler, JwtTokenHandler>();
        }
    }
}
