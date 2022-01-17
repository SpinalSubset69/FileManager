using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FileManager.Api.Extensions;

internal static class ServicesExtensions
{
    public static IServiceCollection AddJwtBearerAuthorizationSchema(this IServiceCollection services, IConfiguration _config)
    {
        //SCHEMA to Authorisation Attribute
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Cryptography:securityKey").Value)),
                    ValidateIssuer =false,
                    ValidateAudience = false
                };
            });

        services.AddCors(op => {
            op.AddPolicy("Cors", options => {
                options.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200");
            });
        });

        return services;
    }
}
