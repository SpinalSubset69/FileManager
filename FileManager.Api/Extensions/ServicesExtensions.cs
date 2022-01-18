using FileManage.DataAccess.Data;
using FileManage.DataAccess.DbAccess;
using FileManage.DataAccess.Interfaces;
using FileManager.Api.Helpers;
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
                //options.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200", "http://localhost:19000", "http://192.168.0.6:19000");
                options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
            });
        });



        return services;
    }

    public static IServiceCollection DependenciesContainerExtension(this IServiceCollection services)
    {

        //Data Access
        services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
        services.AddSingleton<IUnitOfWork, UnitOfWork>();

        //Services
        services.AddScoped(typeof(UserService));
        services.AddScoped(typeof(FolderService));
        services.AddScoped(typeof(AuthService));
        services.AddAutoMapper(typeof(MappingProifle));

        return services;
    }
}
